using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;
using RPG.Saving;

//Starting namespace with RPG. in case I bring in anything with the same namespace later on
namespace RPG.Movement
{
    //Can only inherit from 1 class, but as many interfaces as you would like
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;
        NavMeshAgent navMeshAgent;
        Animator animator;
        Health health;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.HasDied();
            UpdateAnimator();
        }

        //Used to create a distiction between an action starting and just calling MoveTo
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.destination = destination;
        }

        //Used by Fighter to stop player when in range
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            //Grabbing global velocity
            Vector3 velocity = navMeshAgent.velocity;
            //Converts global to local for animator
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;

        }

        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;

            GetComponent<NavMeshAgent>().enabled = false;

            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();

            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
