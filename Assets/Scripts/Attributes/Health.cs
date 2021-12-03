using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;
using RPG.Core;
using RPG.Stats;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        BaseStats baseStats;

        bool hasDied = false;
        public bool HasDied() { return hasDied; }

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
        }

        private void Start()
        {
            healthPoints = baseStats.GetHealth();
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            //Taking the higher of the two values
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / baseStats.GetHealth());
        }

        private void Die()
        {
            if (hasDied) return;

            hasDied = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(baseStats.GetExperienceReward());
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if(healthPoints <= 0)
            {
                Die();
            }
        }
    }
}
