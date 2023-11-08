using System;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private PlayerController2 player;
    private GravityApplier gravityApplier;
    private GameManager gameManager;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController2>();
        gravityApplier = FindObjectOfType<GravityApplier>();
        gameManager = FindObjectOfType<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !player.HasSeed())
        {
            player.seed = this;
            player.isPlantOnMe = true;
            SetSeedParent(player);
        }
        if (other.gameObject.tag == "FinalSpot" && player.HasSeed())
        {
            player.areYouReadyToWin = true;
            SetFinalSeedParent(player);
            player.isPlantOnMe = false;
            gravityApplier.applyGravity = true;
            gameManager.OnVictory();


        }
    }

    public void SetSeedParent(PlayerController2 player)
    {
        transform.parent = player.GetSeedNewTransform();
        transform.localPosition = Vector3.zero;
    }

    public void SetFinalSeedParent(PlayerController2 player)
    {
        transform.parent = player.GetSeedFinalNewTransform();
        transform.localPosition = Vector3.zero;

    }

    public void RemoveSeedParent()
    {
        float xOffset = UnityEngine.Random.Range(-2, 2);
        float zOffset = UnityEngine.Random.Range(-2, 2);
        transform.position = player.transform.position - new Vector3(xOffset, -1.5f, zOffset); // lo iremos mejorando
        player.isPlantOnMe = false;
        player.seed = null;
        transform.parent = null;
        gravityApplier.applyGravity = true;
        
    }




}
