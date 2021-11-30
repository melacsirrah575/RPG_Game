using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {

        SavingSystem savingSystem;
        const string defaultSaveFile = "save";

        private void Start()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                savingSystem.Save(defaultSaveFile);

            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                savingSystem.Load(defaultSaveFile);

            }
        }
    }
}
