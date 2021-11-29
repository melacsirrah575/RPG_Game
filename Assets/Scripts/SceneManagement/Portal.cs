using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [Tooltip("Cannot be less than 0")]
        [SerializeField] int sceneToLoad = -1; //Initialized to -1 to throw error if forgotten to change in editor

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
