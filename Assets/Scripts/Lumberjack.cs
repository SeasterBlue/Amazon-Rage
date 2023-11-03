using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Cinemachine.DocumentationSortingAttribute;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(NavMeshAgent))]
public class Lumberjack : RecyclableObject
{
    private int health;
    private NavMeshAgent navAgent;
    private Animator animator;
    [SerializeField] private Weapon machete;

    internal override void Init()
    {
        
        health = 100;
        if(navAgent == null)
        {
            navAgent = GetComponent<NavMeshAgent>();
            navAgent.updateUpAxis = false;
            navAgent.updateRotation = false;
        }
        if(animator == null)
            animator = GetComponent<Animator>();

        Invoke(nameof(Recycle), 15);
    }

    internal override void Release()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            navAgent.isStopped = false;
            animator.SetBool("walking", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !machete.attacking)
        {
            Vector3 destination = other.gameObject.transform.position;
            navAgent.destination = destination;
            
            Vector3 direction = (destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navAgent.angularSpeed);

            float remainingDistance = Vector3.Distance(transform.position, destination);
            if(remainingDistance <= 0.95f)
            {
                Debug.Log("Lumberjack attacking");
                //navAgent.isStopped = true;
                navAgent.destination = transform.position;
                animator.SetBool("attacking", true);
                machete.attacking = true;
                Invoke("AnimToWalk", 2.267f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            navAgent.destination = transform.position;
            navAgent.isStopped = true;
            animator.SetBool("walking", false);
        }
    }

    public void RecieveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // ANIMATION AND SOUND OF DEAD
            Destroy(gameObject);
        }
    }

    private void AnimToWalk()
    {
        animator.SetBool("attacking", false);
        animator.SetBool("walking", true);
        machete.attacking = false;
    }
}
