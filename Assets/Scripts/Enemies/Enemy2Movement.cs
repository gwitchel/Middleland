using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyBody; 
    public GameObject enemyFlame; 
    public float activeDuration = 2.0f; // Duration for which "Enemy2Attack" is active
    public float inactiveDuration =4.0f; // Duration for which "Enemy2Attack" is inactive
    public float maxDuration = 5.0f; // Maximum duration for moving or stalling
    public float moveSpeed = 1.0f; // Speed of the enemy movement
    private bool movingRight = true;
    public Rigidbody2D rb;
    void Start()
    {
        StartCoroutine(ShowFlame());
        StartCoroutine(MoveAndStallRoutine());
        rb.freezeRotation = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator ShowFlame()
    {
        while (true) // Loop to continuously toggle visibility
        {
            // Hide and deactivate
            enemyFlame.SetActive(false);
            yield return new WaitForSeconds(inactiveDuration);

            // Show and activate
            enemyFlame.SetActive(true);
            yield return new WaitForSeconds(activeDuration);
        }
    }

        private IEnumerator MoveAndStallRoutine()
    {
        while (true) // Loop to continuously move and stall
        {
            // Randomly choose moving direction
            bool newMovingRight = Random.Range(0, 2) == 0;
            if (newMovingRight != movingRight)
            {
                Flip();
            }
            movingRight = newMovingRight;

            // Move for a random duration
            float moveDuration = Random.Range(0f, maxDuration);
            float moveTimer = 0;
            while (moveTimer < moveDuration)
            {
                MoveEnemy(movingRight);
                moveTimer += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Rest for a random duration
            float restDuration = Random.Range(0f, maxDuration);
            yield return new WaitForSeconds(restDuration);
        }
    }

    private void MoveEnemy(bool moveRight)
    {
        // Move the enemy left or right based on 'moveRight'
        float moveDirection = moveRight ? 1 : -1;
        transform.Translate(moveSpeed * moveDirection * Time.deltaTime, 0, 0);
    }

    private void Flip()
    {
          // Find the child object
        Transform bodyTransform = transform.Find("Enemy2Body");
        if (bodyTransform == null)
        {
            Debug.LogError("Child 'enemy2Body' not found.");
            return;
        }

        // Calculate the offset from the parent GameObject
        Vector3 pivotOffset = new Vector3(bodyTransform.position.x - transform.position.x,0, 0);
        Debug.Log(bodyTransform.position.x);
        Debug.Log(transform.position.x);
        Debug.Log(pivotOffset);
        // Move parent GameObject to child's position, rotate, and move back
        transform.position += pivotOffset;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        transform.position -= pivotOffset;
    }
}
