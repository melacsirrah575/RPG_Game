using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] InventorySlotUI InventoryItemPrefab = null;

        Inventory playerInventory;

        private void Awake()
        {
            playerInventory = Inventory.GetPlayerInventory();
            playerInventory.inventoryUpdated += Redraw;
        }

        private void Start()
        {
            Redraw();
        }

        // PRIVATE

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < playerInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(InventoryItemPrefab, transform);
                itemUI.Setup(playerInventory, i);
            }
        }
    }
}
