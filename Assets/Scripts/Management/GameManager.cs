using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    GameObject leaf;
    Transform finalSpot;
    Transform magicPlant;
    public float distanceMagnitude;
    public float minDistance = 2;
    public float maxDistance = 160;
    Renderer plantRenderer;
    Material plantMaterial;



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
        leaf = GameObject.Find("Box037").GetComponent<GameObject>();
        plantRenderer = leaf.GetComponent<Renderer>();
        plantMaterial = plantRenderer.GetComponent<Material>();
    }

    void Update()
    {
        CalcuteDistance();
        ControlBrightnessBasedOnDistance();
    }

    public void CalcuteDistance()
    {
        distanceMagnitude = finalSpot.position.magnitude - magicPlant.position.magnitude;
        
    }

    public void ControlBrightnessBasedOnDistance()
    {
        float brightnessFactor = Mathf.InverseLerp(minDistance, maxDistance, distanceMagnitude);
        Color originalColor = plantMaterial.color;
        Color finalColor = Color.Lerp(originalColor, Color.yellow, brightnessFactor);
        plantMaterial.color = finalColor;

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
