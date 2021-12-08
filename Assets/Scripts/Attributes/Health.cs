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
        [Tooltip("Percent of health player returns to on level up if below")]
        [SerializeField] float regenerationPercentage = 70f;

        float healthPoints = -1f;

        BaseStats baseStats;

        bool isDead = false;
        public bool IsDead() { return isDead; }

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
        }

        private void Start()
        {
            if (healthPoints < 0)
            {
                healthPoints = baseStats.GetStat(Stat.Health);
            }
        }

        private void OnEnable()
        {
            baseStats.onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            baseStats.onLevelUp -= RegenerateHealth;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            //Taking the higher of the two values
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / baseStats.GetStat(Stat.Health));
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(baseStats.GetStat(Stat.ExperienceReward));
        }
        private void RegenerateHealth()
        {
            float regenHealthPoints = baseStats.GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
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
