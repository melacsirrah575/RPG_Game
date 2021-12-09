using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        TextMeshProUGUI text;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            //Updates HUD and is formatted to return 0 decimal places
            text.text = String.Format("{0:0}", baseStats.GetLevel());
        }
    }
}