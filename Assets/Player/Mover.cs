using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    NavMeshAgent navMeshAgent;
    Ray lastRay;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Setting lastRay to where player clicked
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        //Ray is shown in editor
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100);

        navMeshAgent.destination = target.position;
    }
}
