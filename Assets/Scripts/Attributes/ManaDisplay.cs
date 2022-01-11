using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace RPG.Attributes
{
    public class ManaDisplay : MonoBehaviour
    {
        Mana mana;
        TextMeshProUGUI text;

        private void Awake()
        {
            mana = GameObject.FindGameObjectWithTag("Player").GetComponent<Mana>();
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            text.text = String.Format("{0:0}/{1:0}", mana.GetMana(), mana.GetMaxMana());
        }
    }
}
