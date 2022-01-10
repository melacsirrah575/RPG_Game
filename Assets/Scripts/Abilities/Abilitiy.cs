using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Inventories;

namespace RPG.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Ability")]
    public class Abilitiy : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy;

        public override void Use(GameObject user)
        {
            targetingStrategy.StartTargeting();
        }
    }
}

