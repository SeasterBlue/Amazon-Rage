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
        if (other.CompareTag("Player") && !player.HasEgg())
        {
            player.pickedSeed = this;
            SetEggParent(player);
        }
        else if (other.CompareTag("Player") && player.HasEgg())
        {
            Debug.Log("You can only have one egg at a time");
        }
    }

    public void SetEggParent(PlayerController player)
    {
        transform.parent = player.GetEggNewTransform();
        transform.localPosition = Vector3.zero;
    }
    public void RemoveEggParent()
    {
        transform.parent = null;
    }



}
