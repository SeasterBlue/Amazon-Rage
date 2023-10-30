using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    Transform finalSpot;
    Transform magicPlant;

    public float distanceMagnitude;



    public static GameManager Instance
    {
        get { return instance; }
    }

    private bool victoryTriggered = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        finalSpot = GameObject.Find("FinalSpot").GetComponent<Transform>();
        magicPlant = GameObject.Find("MagicPlant").GetComponent<Transform>();
    }

    void Update()
    {
        CalcuteDistance();
    }

    public void CalcuteDistance()
    {
        distanceMagnitude = finalSpot.position.magnitude - magicPlant.position.magnitude;
    }


    public void OnGameOver()
    {
        // Display animation,  UI and button to restart
        Debug.Log("Game Over");
    }

    private void OnVictory()
    {
        
    }

    public void OnSeedPicked()
    {
        
    }




}
