using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    Transform finalSpot;
    Transform magicPlant;
    public float distanceMagnitude;
    public float minDistance = 2;
    public float maxDistance = 160;

    public Light pointLight;



    public static GameManager Instance
    {
        get { return instance; }
    }

    //private bool victoryTriggered = false;

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
        ControlBrightnessBasedOnDistance();
    }

    public void CalcuteDistance()
    {
        distanceMagnitude = Mathf.Abs(finalSpot.position.magnitude - magicPlant.position.magnitude);
    }

    public void ControlBrightnessBasedOnDistance()
    {

        float normalizedDistance = Mathf.Clamp01(distanceMagnitude / 158f);
        float intensity = Mathf.Lerp(1f, 0f, normalizedDistance);
        pointLight.intensity = intensity;
    }


    public void OnGameOver()
    {
        // Display animation,  UI and button to restart
        Debug.Log("Game Over");
    }

    public void OnVictory()
    {
        Debug.Log("Ganaste maldita perra");
    }

    public void OnSeedPicked()
    {
        
    }




}
