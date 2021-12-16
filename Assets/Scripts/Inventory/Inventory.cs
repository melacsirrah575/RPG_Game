using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;

namespace RPG.Inventory
{
    //Should be placed on the Player Gameobject
    public class Inventory : MonoBehaviour, ISaveable
    {
        [Tooltip("Allowed size")]
        [SerializeField] int inventorySize = 16;

        InventoryItem[] slots;

        //Broadcasts when items in the slots are added/removed
        public event Action inventoryUpdated;

        //Convience for getting the player's inventory
        public static Inventory GetPlayerInventory()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<Inventory>();
        }

        public bool HasSpaceFor(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }

        public int GetSize()
        {
            return slots.Length;
        }

        //Attempts to add items to the first available slot.
        //Returns whether or not the item could be added
        public bool AddToFirstEmptySlot(InventoryItem item)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i] = item;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }
        //Checks for instance of the item in the inventory
        public bool HasItem(InventoryItem item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        public InventoryItem GetItemInSlot(int slot)
        {
            return slots[slot];
        }

        public void RemoveFromSlot(int slot)
        {
            slots[slot] = null;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }

        //Will add an item to given slot if possible.
        //If there is already a stack of this type, it will add to existing stack.
        //Otherwise it will be added to the first empty slot.
        //Returns true if the item was added anywhere in the inventory
        public bool AddItemToSlot(int slot, InventoryItem item)
        {
            if (slots[slot] != null)
            {
                return AddToFirstEmptySlot(item);
            }

            slots[slot] = item;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }

        private void Awake()
        {
            slots = new InventoryItem[inventorySize];
            slots[0] = InventoryItem.GetFromID("71e73607-4bac-4e42-b7d6-5e6f91e92dc4");
            slots[1] = InventoryItem.GetFromID("0aa7c8b8-4796-42aa-89d0-9d100ea67d7b");
        }

        //Finds a slot that can accomodate given item. Returns -1 if no slot is found.
        private int FindSlot(InventoryItem item)
        {
            return FindEmptySlot();
        }

        //Finds Empty Slot. Returns -1 if all are full
        private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        object ISaveable.CaptureState()
        {
            var slotStrings = new string[inventorySize];
            for (int i = 0; i < inventorySize; i++)
            {
                if (slots[i] != null)
                {
                    slotStrings[i] = slots[i].GetItemID();
                }
            }
            return slotStrings;
        }

        void ISaveable.RestoreState(object state)
        {
            var slotStrings = (string[])state;
            for (int i = 0; i < inventorySize; i++)
            {
                slots[i] = InventoryItem.GetFromID(slotStrings[i]);
            }
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }
    }
}
