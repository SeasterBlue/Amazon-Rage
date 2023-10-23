using System;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private PlayerController player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !player.HasSeed())
        {
            player.pickedSeed = this;
            SetSeedParent(player);
        }else if (other.CompareTag("Player") && player.HasSeed())
        {
            Debug.Log("Ya la tienes prra");
        }
    }

    public void SetSeedParent(PlayerController player)
    {
        transform.parent = player.GetSeedNewTransform();
        transform.localPosition = Vector3.zero;
    }
    public void RemoveSeedParent()
    {
        transform.parent = null;
    }



}
