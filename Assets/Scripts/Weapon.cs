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

    private AudioSource audioSource;
    private AudioData audioData;
    private AudioClip clip;

    private Lumberjack enemy;
    private PlayerController2 player;
    public bool attacking;
    public bool grabed;

    private void Start()
    {
        audioData = GetComponent<AudioData>();
        audioSource = GetComponent<AudioSource>();

        attacking = false;
        grabed = false;
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

                clip = audioData.attack[Random.Range(1, 3)];
                audioSource.PlayOneShot(clip, 1f);
            }
            else if (targetTag == "Player")
            {
                player = other.gameObject.GetComponent<PlayerController2>();
                player.RecieveDamage(damage);

                clip = audioData.attack[0];
                audioSource.PlayOneShot(clip, 1f);
            }
        }
    }

    IEnumerator DisableChainsawAttack()
    {
        yield return new WaitForSeconds(1.09f);
        attacking = false;
    }

    public void Equiping()
    {
        if (grabed == false)
        {
            Debug.Log("Sonido Grab");

            grabed = true;
            clip = audioData.items[0];
            audioSource.PlayOneShot(clip, 1f);
        }
    }
}
