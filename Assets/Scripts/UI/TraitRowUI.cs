using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using RPG.Stats;

namespace RPG.UI
{
    public class TraitRowUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI valueText;
        [SerializeField] Button minusButton;
        [SerializeField] Button plusButton;
        [SerializeField] Trait trait;

        int value = 0;

        private void Start()
        {
            minusButton.onClick.AddListener(() => Allocate(-1));
            plusButton.onClick.AddListener(() => Allocate(1));
        }

        private void Update()
        {
            minusButton.interactable = value > 0;

            valueText.text = value.ToString();
        }

        public void Allocate(int points)
        {
            value += points;
            if (value < 0)
            {
                value = 0;
            }
        }
    }
}
