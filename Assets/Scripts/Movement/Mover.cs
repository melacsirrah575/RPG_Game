using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;

//Starting namespace with RPG. in case I bring in anything with the same namespace later on
namespace RPG.Movement
{
    //Can only inherit from 1 class, but as many interfaces as you would like
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;
        Animator animator;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        //Used to create a distiction between an action starting and just calling MoveTo
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.isStopped = false;
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

    }
}
