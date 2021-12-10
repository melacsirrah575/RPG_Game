using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI damageText = null;
        public void SetValue(float value)
        {
            damageText.text = String.Format("{0,0}", value);
        }
    }
}
