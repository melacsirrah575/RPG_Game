using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Stats;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = "Inventory/Equipable Stat Item")]
    public class StatsEquipableItem : EquipableItem, IModifierProvider
    {
        [SerializeField] Modifier[] additiveModifier;
        [SerializeField] Modifier[] percentageModifier;

        [System.Serializable]
        struct Modifier
        {
            public Stat stat;
            public float value;
        }
        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (var modifier in additiveModifier)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (var modifier in percentageModifier)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }
    }
}