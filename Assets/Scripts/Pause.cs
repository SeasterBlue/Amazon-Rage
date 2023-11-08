using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject audioObject;

    private List<Weapon> weapons;
    private Seed seed;
    private List<Lumberjack> lumberjacks;
    private PlayerController2 player;

    private AudioSource sfx;

    private bool pause = false;

    private void Start()
    {
        //weapons = new List<Weapon>();
        //weapons.

        sfx = audioObject.GetComponent<AudioSource>(); 
        canvas.SetActive(false);
    }

    private void LateUpdate()
    {
        PauseToggle();
    }

    void PauseToggle()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        //pauseBtn.enabled = false;
        canvas.SetActive(true);

        // Time.timeScale = 0f;

        

        sfx.Play();
        pause = true;
    }

    public void ResumeGame()
    {
        //pauseBtn.enabled = true;
        canvas.SetActive(false);

        // Time.timeScale = 1f;
        Debug.Log("Time Running");

        sfx.Play();
        pause = false;
    }
}
