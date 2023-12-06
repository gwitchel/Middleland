using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantProduction : MonoBehaviour
{
    public float timerDuration = 10.0f; // Duration in seconds, can be set in the Inspector
    public bool readyForHarvest = false;
    public Score score; 
    void Start()
    {
        GameObject protagonist = GameObject.Find("Protagonist");
        score = protagonist.GetComponent<Score>();
        // Start the countdown as soon as the GameObject is active
        StartCoroutine(StartCountdown(timerDuration));
    }

    public void Harvest(){
        Debug.Log("INCREASING SCORE");
        score.IncreaseScore(5);
        readyForHarvest = false;
        StartCoroutine(StartCountdown(timerDuration));
    }
    private IEnumerator StartCountdown(float duration)
    {
        readyForHarvest = false;
        float currentTime = duration;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }

        readyForHarvest = true;
    }
}
