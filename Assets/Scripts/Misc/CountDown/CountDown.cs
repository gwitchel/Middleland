using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public float timeRemaining = 60; // 60 seconds for example
    public bool timerIsRunning = false;
    public LevelLoader levelLoader;
    public TMP_Text timerText; // For TextMeshPro UI
    // public Text text; // Use this instead if you're using standard Unity UI

    private void Start()
    {
        // Start the timer
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                levelLoader.LoadFailedLevel();
                // You can add additional actions here for when the timer ends.
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // If using standard UI, replace 'timerText' with 'text'
    }
}

