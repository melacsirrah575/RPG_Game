using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        TextMeshProUGUI text;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            //Updates HUD and is formatted to return 0 decimal places
            text.text = String.Format("{0:0}", experience.GetPoints());
        }
    }
}