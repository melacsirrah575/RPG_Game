using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using RPG.Inventories;
using RPG.Control;

namespace RPG.Shops
{
    public class Shop : MonoBehaviour, IRaycastable
    {
        [SerializeField] string shopName;
        [Tooltip("Reduces amount item will be bought back for")]
        [Range(0, 100)]
        [SerializeField] float sellingPercentage = 80f;
        [SerializeField] StockItemConfig[] stockConfig;

        [System.Serializable]
        class StockItemConfig
        {
            public InventoryItem item;
            public int initialStock;
            [Range(0, 100)]
            public float buyingDiscountPercent;
        }

        Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();
        Dictionary<InventoryItem, int> stock = new Dictionary<InventoryItem, int>();
        Shopper currentShopper = null;
        bool isBuyingMode = true;

        public event Action onChange;

        private void Awake()
        {
            foreach (StockItemConfig confing in stockConfig)
            {
                stock[confing.item] = confing.initialStock;
            }
        }

        public void SetShopper(Shopper shopper)
        {
            this.currentShopper = shopper;
        }

        public IEnumerable<ShopItem> GetFilteredItems() 
        {
            return GetAllItems();
        }

        public IEnumerable<ShopItem> GetAllItems()
        {
            foreach (StockItemConfig config in stockConfig)
            {
                float price = GetPrice(config);
                int quantityInTransaction = 0;
                transaction.TryGetValue(config.item, out quantityInTransaction);
                int availability = GetAvailability(config.item);
                yield return new ShopItem(config.item, availability, price, quantityInTransaction);
            }
        }

        public void SelectFilter(ItemCategory category) { }
        public ItemCategory GetFilter() { return ItemCategory.None; }
        public void SelectMode(bool isBuying) 
        {
            isBuyingMode = isBuying;
            if (onChange != null)
            {
                onChange();
            }
        }

        public bool IsBuyingMode() 
        {
            return isBuyingMode; 
        }

        public bool CanTransact() 
        {
            if (IsTransactionEmpty()) return false;
            if (!HasSufficientFunds()) return false;
            if (!HasInventorySpace()) return false;
            return true;
        }

        public bool HasSufficientFunds()
        {
            Wallet wallet = currentShopper.GetComponent<Wallet>();
            if (wallet == null) return false;

            return wallet.GetBalance() >= TransactionTotal();
        }

        public bool IsTransactionEmpty()
        {
            return transaction.Count == 0;
        }

        public bool HasInventorySpace()
        {
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            if (shopperInventory == null) return false;

            List<InventoryItem> flatItems = new List<InventoryItem>();
            foreach (ShopItem shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                for (int i = 0; i < quantity; i++)
                {
                    flatItems.Add(item);
                }
            }

            return shopperInventory.HasSpaceFor(flatItems);
        }

        public void ConfirmTransaction() 
        {
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            Wallet shopperWallet = currentShopper.GetComponent<Wallet>();
            if (shopperInventory == null || shopperWallet == null) return;

            foreach (ShopItem shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                float price = shopItem.GetPrice();

                for (int i = 0; i < quantity; i++)
                {
                    if (shopperWallet.GetBalance() < price) break;

                    bool success = shopperInventory.AddToFirstEmptySlot(item, 1);
                    if (success)
                    {
                        AddToTransaction(item, -1);
                        stock[item]--;
                        shopperWallet.UpdateBalance(-price);
                    }
                }
            }

            if (onChange != null)
            {
                onChange();
            }
        }

        public string GetShopName()
        {
            return shopName;
        }

        public float TransactionTotal() 
        {
            float total = 0;
            foreach (ShopItem item in GetAllItems())
            {
                total += item.GetPrice() * item.GetQuantityInTransaction();
            }
            return total;
        }
        public void AddToTransaction(InventoryItem item, int quantity) 
        {
            if (!transaction.ContainsKey(item))
            {
                transaction[item] = 0;
            }

            int availability = GetAvailability(item);
            if (transaction[item] + quantity > availability)
            {
                transaction[item] = availability;
            } 
            else
            {
                transaction[item] += quantity;
            }

            if (transaction[item] <= 0)
            {
                transaction.Remove(item);
            }

            if (onChange != null)
            {
                onChange();
            }
        }

        public CursorType GetCursorType()
        {
            return CursorType.Shop;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Shopper>().SetActiveShop(this);
            }
            return true;
        }

        private int GetAvailability(InventoryItem item)
        {
            if (isBuyingMode)
            {
                return stock[item];
            }

            return CountItemsInInventory(item);
        }

        private int CountItemsInInventory(InventoryItem item)
        {
            Inventory inventory = currentShopper.GetComponent<Inventory>();
            if (inventory == null) return 0;

            int total = 0;
            for (int i = 0; i < inventory.GetSize(); i++)
            {
                if (inventory.GetItemInSlot(i) == item)
                {
                    total += inventory.GetNumberInSlot(i);
                }
            }
            return total;
        }

        private float GetPrice(StockItemConfig config)
        {
            if (isBuyingMode)
            {
                return config.item.GetPrice() * (1 - config.buyingDiscountPercent / 100);
            }

            return config.item.GetPrice() * (sellingPercentage / 100);
        }
    }
}
