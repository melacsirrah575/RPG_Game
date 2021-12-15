using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

using RPG.Saving;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [Tooltip("Cannot be less than 0")]
        [SerializeField] int sceneToLoad = -1; //Initialized to -1 to throw error if forgotten to change in editor
        [SerializeField] Transform spawnPoint;
        [SerializeField] [Range(0f, 5f)] float fadeOutTime = 1f;
        [SerializeField] [Range(0f, 5f)] float fadeInTime = 1f;
        [SerializeField] [Range(0f, 5f)] float fadeWaitTime = 0.5f;
        [SerializeField] DestinationIdentifier destination;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("You forgot to assign a scene to load.");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

            playerController.enabled = false;

            yield return fader.FadeOut(fadeOutTime);
            savingWrapper.Save();

            //Calls Coroutine again once scene has finished loading
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newPlayerController.enabled = false;
            
            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            //Saving here to prevent player spawning in portal and teleporting if walked through a portal and didn't manually save before quitting
            savingWrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn(fadeInTime);

            newPlayerController.enabled = true;
            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }

            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            //Using NavMeshAgent to prevent conflict between it and manual placement
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
