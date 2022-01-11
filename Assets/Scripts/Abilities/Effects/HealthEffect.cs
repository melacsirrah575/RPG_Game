using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Attributes;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu (menuName ="Abilities/Effects/Health")]
    public class HealthEffect : EffectStrategy
    {
        [SerializeField] float healthChange;
        public override void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action finished)
        {
            foreach (var target in targets)
            {
                var health = target.GetComponent<Health>();
                if (health)
                {
                    if (healthChange < 0)
                    {
                        health.TakeDamage(user, -healthChange);
                    }
                    else
                    {
                        health.Heal(healthChange);
                    }
                }
            }
            finished();
        }
    }
}
