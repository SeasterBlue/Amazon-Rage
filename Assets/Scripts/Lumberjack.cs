using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(NavMeshAgent))]
public class Lumberjack : RecyclableObject
{
    private int health;
    private NavMeshAgent navAgent;

    internal override void Init()
    {
        health = 100;
        Invoke(nameof(Recycle), 15);
        if(navAgent == null)
            navAgent = GetComponent<NavMeshAgent>();
    }

    internal override void Release()
    {
        // TODO
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            navAgent.isStopped = false;
            navAgent.destination = other.gameObject.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            navAgent.isStopped = true;
    }
}
