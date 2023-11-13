using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private int timer = 0; 
    private Animator anim; 
    private SpriteRenderer spriteRenderer;
    // the amount of time is takes for the plant to spawn a new enemy 
    public int primaryAnimationFrames;
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        updateAnimationState();
    }

    public void updateAnimationState()
    {
        // Increment the frame counter
        timer++;

        Debug.Log(timer);
        Debug.Log(primaryAnimationFrames);
        // Check if it's time to play the secondary animation
        if (timer >= primaryAnimationFrames)
        {
            // Play the secondary animation
            anim.Play("PlantSpawn");

            // Reset the frame counter
            timer = 0;

            // Wait for the secondary animation to finish before returning to the primary animation
            StartCoroutine(WaitForSpawn());
        }
    }

    private IEnumerator WaitForSpawn()
    {
        // Disable the script temporarily
        enabled = false;

        // Wait for the length of the secondary animation
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // Return to the primary animation
        anim.Play("PlantMove");

        // Enable the script
        enabled = true;
    }

}
