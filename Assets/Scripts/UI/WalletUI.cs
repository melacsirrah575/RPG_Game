using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using RPG.Inventories;

namespace RPG.UI.Inventories 
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI balanceField;

        Wallet playerWallet = null;

        private void Start()
        {
            playerWallet = GameObject.FindGameObjectWithTag("Player").GetComponent<Wallet>();

            if (playerWallet != null)
            {
                playerWallet.onChange += RefreshUI;
            }

            RefreshUI();
        }

        private void RefreshUI()
        {
            balanceField.text = $"${playerWallet.GetBalance():N2}";
        }
    }
}

