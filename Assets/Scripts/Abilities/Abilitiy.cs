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
        [SerializeField] EffectStrategy[] effectStrategies;

        public override void Use(GameObject user)
        {
            targetingStrategy.StartTargeting(user,
                (IEnumerable<GameObject> targets) =>
                {
                    TargetAquired(user, targets);
                });
        }

        private void TargetAquired(GameObject user, IEnumerable<GameObject> targets)
        {
            foreach (var filterStrategy in filterStrategies)
            {
                targets = filterStrategy.Filter(targets);
            }

            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(user, targets, EffectFinished);
            }
        }

        private void EffectFinished()
        {

        }
    }
}

