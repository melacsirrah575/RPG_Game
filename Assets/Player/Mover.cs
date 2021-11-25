using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
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
}
