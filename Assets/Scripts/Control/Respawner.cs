using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

using RPG.Attributes;
using RPG.SceneManagement;

namespace RPG.Control
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] Transform respawnLocation;
        [SerializeField] float respawnDelay = 3f;
        [SerializeField] float fadeTime = 0.2f;
        [SerializeField] int healthRegenPercent = 20;
        private void Awake()
        {
            GetComponent<Health>().onDie.AddListener(Respawn);
            respawnLocation = transform;
        }

        private void Respawn()
        {
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(respawnDelay);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime);
            GetComponent<NavMeshAgent>().Warp(respawnLocation.position);
            Health health = GetComponent<Health>();
            health.Heal(health.GetMaxHealthPoints() * healthRegenPercent / 100);
            yield return fader.FadeIn(fadeTime);
        }
    }
}
