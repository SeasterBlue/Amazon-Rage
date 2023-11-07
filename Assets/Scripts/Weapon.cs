using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(Collider))]
public class Weapon : MonoBehaviour
{
    public int damage;
    [SerializeField] private string targetTag;
    [SerializeField] AudioClip attackSfx;
    [SerializeField] AudioClip grabSfx;
        
    private AudioSource audioSource;

    private Lumberjack enemy;
    private PlayerController2 player;
    // private Collider collider;
    private Rigidbody rb;
    public bool attacking;

    private void Start()
    {
        // collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        rb= GetComponent<Rigidbody>();
        attacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attacking == true)
        {
            Attacking(other);
            if(!targetTag.Equals("Lumberjack"))
                attacking = false;
            else
                StartCoroutine(DisableChainsawAttack());
        }
    }

    public void Attacking(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (targetTag == "Lumberjack")
            {
                enemy = other.gameObject.GetComponent<Lumberjack>();
                enemy.RecieveDamage(damage);
            } 
            else if(targetTag == "Player")
            {
                player = other.gameObject.GetComponent<PlayerController2>();
                player.RecieveDamage(damage);
            }
            audioSource.Play();
        }
    }

    IEnumerator DisableChainsawAttack()
    {
        yield return new WaitForSeconds(1.09f);
        attacking = false;
    }
}
