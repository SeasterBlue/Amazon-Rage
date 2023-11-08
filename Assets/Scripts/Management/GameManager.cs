using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    Transform finalSpot;
    Transform magicPlant;
    public float distanceMagnitude;
    public float minDistance = 2;
    public float maxDistance = 160;

    public Light pointLight;
    public PlayableDirector timelineDirector;

    public GameObject canvasDeath;
    private Animator player;



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
        player = GameObject.Find("Player").GetComponent<Animator>();
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
        player.SetBool("isDead", true);
        StartCoroutine(DelayChangeScene());

    }

    IEnumerator DelayChangeScene()
    {
        yield return new WaitForSeconds(2.0f);
        canvasDeath.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(0);
    }

    public void OnVictory()
    {
        Debug.Log("Ganaste maldita perra");
        PlayFinalCinematic();
    }

    public void PlayFinalCinematic()
    {
        if (timelineDirector != null)
        {
            // Play the Timeline when the script starts
            timelineDirector.Play();
        }
        else
        {
            Debug.LogError("PlayableDirector not assigned!");
        }
    }

    public void LoadInitialScene()
    {
        SceneManager.LoadScene(0);
    }

    




}
