using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] GameObject leftFoot;
    [SerializeField] GameObject rightFoot;
    PlayerController2 player;
    void Start()
    {
       player = FindObjectOfType<PlayerController2>();
    }


    void ActiveParticleLeftFoot()
    {
        if(player.isGrounded) leftFoot.SetActive(true);

    }

    void ActiveParticleRightFoot()
    {
        if (player.isGrounded) rightFoot.SetActive(true);
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
