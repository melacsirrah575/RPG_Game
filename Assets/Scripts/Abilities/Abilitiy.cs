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
        [SerializeField] float cooldownTime = 0;

        public override void Use(GameObject user)
        {
            CooldownStore cooldownStore = user.GetComponent<CooldownStore>();
            if (cooldownStore.GetTimeRemaining(this) > 0) return;

            AbilityData data = new AbilityData(user);
            targetingStrategy.StartTargeting(data,
                () =>
                {
                    TargetAquired(data);
                });
        }

        private void TargetAquired(AbilityData data)
        {
            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();
            cooldownStore.StartCooldown(this, cooldownTime);

            foreach (var filterStrategy in filterStrategies)
            {
                data.SetTargets(filterStrategy.Filter(data.GetTargets()));
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

