using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(NavMeshAgent))]
public class Lumberjack : RecyclableObject
{
    [SerializeField] private int health;
    private NavMeshAgent navAgent;
    private Animator animator;
    [SerializeField] private Weapon machete;
    private bool attacking = false;
    [SerializeField] private GameObject bloodParticles;

    #region audio
    AudioSource audioSource;
    AudioData audioData;
    AudioClip clip;
    bool cry;
    #endregion
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioData = gameObject.GetComponent<AudioData>();
    }

    internal override void Init()
    {

        health = 100;
        if (navAgent == null)
        {
            navAgent = GetComponent<NavMeshAgent>();
            navAgent.updateUpAxis = false;
            navAgent.updateRotation = false;
        }
        if (animator == null)
            animator = GetComponent<Animator>();

    }

    internal override void Release()
    {
        // Set animator parameters back to initial state
        animator.SetBool("attacking", false);
        animator.SetBool("walking", false);
        animator.SetInteger("health", 100);
        ParticleSystem[] particles = bloodParticles.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particles)
        {
            ps.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            navAgent.isStopped = false;
            animator.SetBool("walking", true);

            if (cry == false)
            {
                cry = true;
                clip = audioData.attack[0];
                audioSource.PlayOneShot(clip, 6f);
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !attacking)
        {
            Vector3 destination = other.gameObject.transform.position;
            navAgent.destination = destination;

            Vector3 direction = (destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navAgent.angularSpeed);

            float remainingDistance = Vector3.Distance(transform.position, destination);
            if (remainingDistance <= 0.95f)
            {
                navAgent.destination = transform.position;
                animator.SetBool("attacking", true);
                machete.attacking = true;
                attacking = true;
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
        animator.SetInteger("health", health);
        if (health <= 0)
        {
            clip = audioData.dead[0];
            audioSource.PlayOneShot(clip, 1f);

            ParticleSystem[] particles = bloodParticles.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem ps in particles)
            {
                ps.Play();
            }
            Invoke(nameof(Recycle), 3);
        }
    }

    private void AnimToWalk()
    {
        animator.SetBool("attacking", false);
        animator.SetBool("walking", true);
        attacking = false;
    }

    public void PlayStep()
    {
        clip = audioData.steps[Random.Range(0, 3)];
        audioSource.PlayOneShot(clip, 1f);
    }

}
