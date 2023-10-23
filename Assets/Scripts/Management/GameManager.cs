using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int eggsDropped = 0;
    [SerializeField] int eggsGoal = 1;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public event EventHandler GameOver;
    public event EventHandler Victory;
    public event EventHandler EggPicked;

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

        OnEggPicked();
    }

    void Start()
    {
        StartCoroutine(StartTimer(timerDurationInMinutes));
    }

    void Update()
    {
        if (eggsDropped == eggsGoal && !victoryTriggered)
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

    public void OnEggPicked()
    {
        EggPicked?.Invoke(this, EventArgs.Empty);
    }




}
