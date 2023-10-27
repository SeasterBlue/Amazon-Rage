using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(NavMeshAgent))]
public class Lumberjack : RecyclableObject
{
    private int health;
    private NavMeshAgent navAgent;
    private Animator animator;

    internal override void Init()
    {
        health = 100;
        Invoke(nameof(Recycle), 15);
        if(navAgent == null)
            navAgent = GetComponent<NavMeshAgent>();
        if(animator == null)
            animator = GetComponent<Animator>();
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
            //navAgent.updateRotation = true;
            animator.SetBool("walking", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Vector3 destination = other.gameObject.transform.position;
            navAgent.destination = destination;
            
            Vector3 direction = (destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navAgent.angularSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            navAgent.destination = transform.position;
            navAgent.isStopped = true;
            //navAgent.updateRotation = false;
            animator.SetBool("walking", false);
        }
    }
}
