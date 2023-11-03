using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button resumeBtn;
    private bool pause = false;

    private void LateUpdate()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!pause)
            {
                pause = true;
                pauseBtn.onClick.Invoke();
            } else
            {
                resumeBtn.onClick.Invoke();
                pause = false;
            }
        }
    }

    public void PauseGame()
    {
        // Time.timeScale = 0f;
        Debug.Log("Time Pause");
    }

    public void ResumeGame()
    {
        // Time.timeScale = 1f;
        Debug.Log("Time Running");
    }
}
