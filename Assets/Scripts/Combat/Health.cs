using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100f;

        float healthPoints;
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

        private void Die()
        {
            if (hasDied) return;

            hasDied = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
