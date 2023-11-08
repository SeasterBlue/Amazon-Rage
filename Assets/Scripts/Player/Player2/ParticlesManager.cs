using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] GameObject leftFoot;
    [SerializeField] GameObject rightFoot;
    PlayerController2 player;

    AudioData audioData;
    AudioSource audioSource;
    AudioClip audioClip;
    void Start()
    {
       audioSource = GetComponent<AudioSource>();
       audioData = GetComponent<AudioData>();
       player = FindObjectOfType<PlayerController2>();
    }


    void ActiveParticleLeftFoot()
    {
        if(player.isGrounded) leftFoot.SetActive(true);

        audioClip = audioData.steps[UnityEngine.Random.Range(0, 2)];
        audioSource.PlayOneShot(audioClip, 0.6f);
    }

    void ActiveParticleRightFoot()
    {
        if (player.isGrounded) rightFoot.SetActive(true);

        audioClip = audioData.steps[UnityEngine.Random.Range(0, 2)];
        audioSource.PlayOneShot(audioClip, 0.6f);
    }

    void NoActiveParticleLeftFoot()
    {
        leftFoot.SetActive(false);
    }

    void NoActiveParticleRightFoot()
    {
        rightFoot.SetActive(false);
    }
}
