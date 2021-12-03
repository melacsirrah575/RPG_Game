using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool hasDied = false;
        public bool HasDied() { return hasDied; }

        private void Start()
        {
            healthPoints = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            //Taking the higher of the two values
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetHealth());
        }

        private void Die()
        {
            if (hasDied) return;

            hasDied = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
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
