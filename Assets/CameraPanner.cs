using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanner : MonoBehaviour
{
    public float y1 = 5.0f; // Number of units to pan the camera
    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position of the camera
        originalPosition = transform.position;
    }

    void Update()
    {
        // Check if the down key is being held down
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Pan the camera down
            PanCamera(-y1);
        }
        // Check if the down key has been released
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            // Pan the camera back up to the original position
            PanCamera(y1);
        }
    }

    void PanCamera(float yOffset)
    {
        // Move the camera up or down
        Vector3 newPosition = transform.position + new Vector3(0, yOffset, 0);
        transform.position = newPosition;
    }
}