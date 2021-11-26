using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100f;

        float health;

        private void Start()
        {
            health = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            //Taking the higher of the two values
            health = Mathf.Max(health - damage, 0);
        }

    }
}
