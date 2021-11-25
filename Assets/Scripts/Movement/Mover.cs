using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        //Takes position from where player clicked on MainCamera's near clipping plane and sets as variable
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //out hit stores Raycast hit position in hit
        bool hasHit = Physics.Raycast(ray, out hit);

        if(hasHit)
        {
            navMeshAgent.destination = hit.point;
        }
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
