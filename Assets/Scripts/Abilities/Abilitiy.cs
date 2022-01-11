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
            AbilityData data = new AbilityData(user);
            targetingStrategy.StartTargeting(data,
                () =>
                {
                    TargetAquired(data);
                });
        }

        private void TargetAquired(AbilityData data)
        {
            foreach (var filterStrategy in filterStrategies)
            {
                data.SetTargets() = filterStrategy.Filter(data.GetTargets());
            }

            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }
        }

        private void EffectFinished()
        {

        }
    }
}

