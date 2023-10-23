using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int seedDropped = 0;
    [SerializeField] int seedsGoal = 1;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public event EventHandler GameOver;
    public event EventHandler Victory;
    public event EventHandler SeedPicked;

    [SerializeField] private float timerDurationInMinutes = 5f;
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

        OnSeedPicked();
    }

    void Start()
    {
        StartCoroutine(StartTimer(timerDurationInMinutes));
    }

    void Update()
    {
        if (seedDropped == seedsGoal && !victoryTriggered)
        {
            victoryTriggered = true;
            OnVictory();
        }
    }

    IEnumerator StartTimer(float durationInMinutes)
    {
        float durationInSeconds = durationInMinutes * 60;

        while (durationInSeconds > 0)
        {
            yield return new WaitForSeconds(1f);
            durationInSeconds -= 1;
        }

        OnGameOver();
    }

    private void OnGameOver()
    {
        GameOver?.Invoke(this, EventArgs.Empty);
    }

    private void OnVictory()
    {
        Victory?.Invoke(this, EventArgs.Empty);
    }

    public void OnSeedPicked()
    {
        SeedPicked?.Invoke(this, EventArgs.Empty);
    }




}
