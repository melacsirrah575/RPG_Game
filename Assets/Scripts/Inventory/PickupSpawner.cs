using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;
using System;

namespace RPG.Inventories {
    //Spawns pickups that should exist on the first load in a level.
    //This automatically spawns the correct prefab for a given inventory item
    public class PickupSpawner : MonoBehaviour, ISaveable
    {
        [SerializeField] InventoryItem item = null;
        [SerializeField] int number = 1;

        private void Awake()
        {
            //Spawned here so it can be destroyed by save system after
            SpawnPickup();
        }

        public Pickup GetPickup()
        {
            return GetComponentInChildren<Pickup>();
        }

        public bool IsCollected()
        {
            return GetPickup() == null;
        }

        private void SpawnPickup()
        {
            var spawnedPickup = item.SpawnPickup(transform.position, number);
            spawnedPickup.transform.SetParent(transform);
        }

        private void DestroyPickup()
        {
            if (GetPickup())
            {
                Destroy(GetPickup().gameObject);
            }
        }

        public object CaptureState()
        {
            return IsCollected();
        }

        public void RestoreState(object state)
        {
            bool shouldBeCollected = (bool)state;

            if (shouldBeCollected && !IsCollected())
            {
                DestroyPickup();
            }

            if (!shouldBeCollected && IsCollected())
            {
                SpawnPickup();
            }
        }
    }
}