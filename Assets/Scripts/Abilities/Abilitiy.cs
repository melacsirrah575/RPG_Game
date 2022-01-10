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
        [SerializeField] FilterStrategy[] filterStrategies;

        public override void Use(GameObject user)
        {
            targetingStrategy.StartTargeting(user, TargetAquired);
        }

        private void TargetAquired(IEnumerable<GameObject> targets)
        {
            foreach (var filterStrategy in filterStrategies)
            {
                targets = filterStrategy.Filter(targets);
            }

            foreach (var target in targets)
            {

            }
        }
    }
}

