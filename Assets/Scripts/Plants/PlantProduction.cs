using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantProduction : MonoBehaviour
{
    public float timerDuration = 10.0f; // Duration in seconds, can be set in the Inspector
    public bool readyForHarvest = false;

    void Start()
    {
        // Start the countdown as soon as the GameObject is active
        StartCoroutine(StartCountdown(timerDuration));
    }

    public void Harvest(){
        
    }
    private IEnumerator StartCountdown(float duration)
    {
        // Wait for the duration to pass
        yield return new WaitForSeconds(duration);

        // Set readyForHarvest to true after the countdown
        readyForHarvest = true;
        Debug.Log("Harvest is now ready.");
    }
}
