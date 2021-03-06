using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;
using System;
using RPG.Core;

namespace RPG.Inventories
{
    public class Equipment : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        Dictionary<EquipLocation, EquipableItem> equippedItems = new Dictionary<EquipLocation, EquipableItem>();

        public event Action equipmentUpdated;

        public EquipableItem GetItemInSlot(EquipLocation equipLocation)
        {
            if (!equippedItems.ContainsKey(equipLocation))
            {
                return null;
            }

            return equippedItems[equipLocation];
        }

        public void AddItem(EquipLocation slot, EquipableItem item)
        {
            //Shouldn't ever really be called but is here for precautionary purposes
            Debug.Assert(item.CanEquip(slot, this));

            equippedItems[slot] = item;

            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }
        }

        public void RemoveItem(EquipLocation slot)
        {
            equippedItems.Remove(slot);
            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }
        }

        public IEnumerable<EquipLocation> GetAllPopulatedSlots()
        {
            return equippedItems.Keys;
        }

        public object CaptureState()
        {
            var equippedItemsForSerialization = new Dictionary<EquipLocation, string>();
            foreach ( var pair in equippedItems)
            {
                equippedItemsForSerialization[pair.Key] = pair.Value.GetItemID();
            }
            return equippedItemsForSerialization;
        }

        public void RestoreState(object state)
        {
            equippedItems = new Dictionary<EquipLocation, EquipableItem>();

            var equippedItemsForSerialization = (Dictionary<EquipLocation, string>)state;

            foreach (var pair in equippedItemsForSerialization)
            {
                var item = (EquipableItem)InventoryItem.GetFromID(pair.Value);
                if (item != null)
                {
                    //pair.Key is just the equipeed item's location it is set to
                    equippedItems[pair.Key] = item;
                }
            }

            equipmentUpdated?.Invoke();
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            if (predicate == "HasItemEquipped")
            {
                foreach (var item in equippedItems.Values)
                {
                    if (item.GetItemID() == parameters[0])
                    {
                        return true;
                    }
                }
                return false;
            }
            return null;
        }
    }
}
