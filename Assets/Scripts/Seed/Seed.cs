using System;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private PlayerController2 player;
    private GravityApplier gravityApplier;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController2>();
        gravityApplier = FindObjectOfType<GravityApplier>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !player.HasSeed())
        {
            player.seed = this;
            SetSeedParent(player);
        }   else if (other.CompareTag("Player") && player.HasSeed())
        {
            Debug.Log("Ya la tienes prra");
        }
    }

    public void SetSeedParent(PlayerController2 player)
    {
        transform.parent = player.GetSeedNewTransform();
        transform.localPosition = Vector3.zero;
    }
    public void RemoveSeedParent()
    {
        transform.position = player.transform.position - new Vector3(0, -1.5f, -2.0f); // lo iremos mejorando
        player.seed = null;
        transform.parent = null;
        gravityApplier.applyGravity = true;
        
    }




}
