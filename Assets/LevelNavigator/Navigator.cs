using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Navigator : MonoBehaviour
{
    public float panSpeed = 5.0f; // Adjust this value for the desired panning speed.
    public float panDistance = 15.0f; // Adjust this value for the desired pan distance.
    
    public GameObject targetButton;

    public int level = 2; 

    public int direction = 0; 
    void Update()
    {
        // Wait for the user to press the Return/Enter key.
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            direction -= 1; 
            if(direction == -2) direction = -1; 
            updateTargetButton();

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            direction += 1;
            if(direction == 2) direction = 1; 
            updateTargetButton();

        }
        if (Input.GetKeyDown(KeyCode.Return) && direction!=0)
        {
            // Check the value of the static variable "foo."
            if (direction == -1)
            {
                // Smoothly pan the camera to the left.
                Vector3 targetPosition = transform.position + Vector3.left * panDistance;
                StartCoroutine(PanCamera(targetPosition));
            }
            else if (direction == 1)
            {
                // Smoothly pan the camera to the right.
                Vector3 targetPosition = transform.position + Vector3.right * panDistance;
                StartCoroutine(PanCamera(targetPosition));
            }
        }

        CheckReplayLevel();
    }
    
    public void CheckReplayLevel() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && direction == 0)
        {
    
            SceneManager.LoadScene("Level2");
               
        }
    }

    

    public void updateTargetButton(){
        float scaleFactor = 1.2f;
        if (targetButton != null){
            targetButton.transform.localScale *= 1/scaleFactor;
        }
        
        if (direction == -1)
        {
            targetButton = GameObject.Find("/Main Camera/LevelDown");
        }
        else if (direction == 0)
        {
            targetButton = GameObject.Find("/Main Camera/replay");
        }
        else if (direction == 1)
        {
            targetButton = GameObject.Find("/Main Camera/LevelUp");
        }
        
        if (targetButton != null){
            targetButton.transform.localScale *= scaleFactor;
        }

    }
    
    public void UpdatedSelectedAnimation( ){

    }

    IEnumerator PanCamera(Vector2 targetPosition)
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (Time.time - startTime < panSpeed)
        {
            float journeyLength = Vector2.Distance(startPosition, targetPosition);
            float distanceCovered = (Time.time - startTime) * panSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector2.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        // Ensure the camera reaches the exact target position.
        transform.position = targetPosition;
    }

  
}