using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        TextMeshProUGUI text;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if(fighter.GetTarget() == null)
            {
                text.text = "N/A";
                return;
            }
            Health health = fighter.GetTarget();
            //Updates HUD and is formatted to return 0 decimal places
            text.text = String.Format("{0:0}%", health.GetPercentage());
        }
    }
}