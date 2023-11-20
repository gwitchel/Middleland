using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanner : MonoBehaviour
{
    public float y1 = 8.0f; // Number of units to pan the camera
    private Vector3 originalPosition;

    private bool movedDown = false; 
    void Start()
    {
        // Store the original position of the camera
        originalPosition = transform.position;
    }

    void Update()
    {
        // Check if the down key is being held down
        if (Input.GetKeyDown(KeyCode.DownArrow) && Mathf.Abs(transform.parent.GetComponent<Rigidbody2D>().velocity.y) < 0.1f )
        {
            // Pan the camera down
            movedDown = true; 
            PanCamera(-y1);
        }
        // Check if the down key has been released
        else if (Input.GetKeyUp(KeyCode.DownArrow) && movedDown)
        {
            // Pan the camera back up to the original position
            PanCamera(y1);
            movedDown = false; 
        }
    }

    void PanCamera(float yOffset)
    {
        // Move the camera up or down
        Vector3 newPosition = transform.position + new Vector3(0, yOffset, 0);
        transform.position = newPosition;
    }
}