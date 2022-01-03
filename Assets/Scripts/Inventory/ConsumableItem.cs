using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Stats;
using RPG.Attributes;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = "Inventory/ConsumableItem")]
    public class ConsumableItem : ActionItem
    {
        [SerializeField] int consumeValue = 0;
        [SerializeField] Stat stat;

        public override void Use(GameObject user)
        {
            base.Use(user);

            if(stat == Stat.Health)
            {
                if (consumeValue > 0)
                {
                    user.GetComponent<Health>().Heal(consumeValue);
                }
            }
        }
    }
}
