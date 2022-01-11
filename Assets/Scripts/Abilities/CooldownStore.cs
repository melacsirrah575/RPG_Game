using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    public class CooldownStore : MonoBehaviour
    {
        Dictionary<Abilitiy, float> cooldownTimers = new Dictionary<Abilitiy, float>();

        private void Update()
        {
            var keys = new List<Abilitiy>(cooldownTimers.Keys);
            foreach (Abilitiy ability in keys)
            {
                cooldownTimers[ability] -= Time.deltaTime;
                if (cooldownTimers[ability] < 0)
                {
                    cooldownTimers.Remove(ability);
                }
            } 
        }

        public void StartCooldown(Abilitiy ability, float cooldownTime)
        {
            cooldownTimers[ability] = cooldownTime;
        }

        public float GetTimeRemaining(Abilitiy ability)
        {
            if (!cooldownTimers.ContainsKey(ability))
            {
                return 0;
            }

            return cooldownTimers[ability];
        }
    }
}
