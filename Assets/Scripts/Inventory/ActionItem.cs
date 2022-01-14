using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu (menuName = "Inventory/Action Item")]
    public class ActionItem : InventoryItem
    {
        [Tooltip("Does an instance of this item get consumed every time it's used")]
        [SerializeField] bool consumable = false;

        public virtual bool Use(GameObject user)
        {
            Debug.Log("using action: " + this);
            return false;
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }
}
