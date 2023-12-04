using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_01_movement : EnemyMovement
{
    private float speed = 2f;
    private bool move = false;
    public Rigidbody2D rb;
    private Animator anim; 
    private SpriteRenderer spriteRenderer;
    private GameObject protagonist; 
    public bool begunDamage = false; 
    private float direction = 1;
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
        protagonist =  GameObject.Find("/Protagonist");

        StartCoroutine(ToggleMoveRoutine());
    }

    
    // Update is called once per frame
    void Update()
    {
        updateAnimationState();
    }

    public void updateAnimationState(){

        if (anim.GetLayerWeight(anim.GetLayerIndex("Damage")) == 1 && !begunDamage)
        {
            begunDamage = true;
            DamageBounce(); 
        }
        else if (anim.GetLayerWeight(anim.GetLayerIndex("Damage")) != 1 && begunDamage)
        {
            begunDamage = false; 
        }

        else if (anim.GetLayerWeight(anim.GetLayerIndex("Damage")) == 0) 
        {
            if(move)
            {
                Move();
            }
            else
            {
                anim.Play("enemy_01_idle");
                rb.velocity = new Vector2(0,0);
            }
        }



    }
    public void DamageBounce()
    {
            if((protagonist.transform.position-this.transform.position).x>0) 
            {
                // positive: attacker is to right (bounce left )
                rb.velocity = new Vector2(-5,10);
            }
            else
            {
                // negative: attacker is to left (bounce right )
                rb.velocity = new Vector2(rb.velocity.x+5,rb.velocity.y+10);
            } 
    }
    
    public void Move()
    {
        anim.Play("enemy_01_walk");
        // Randomly select a direction: 1 for right, -1 for left

        // Move the GameObject
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

    }

    private IEnumerator ToggleMoveRoutine()
    {
        while (true)
        {
            // Toggle the 'move' boolean
            move = !move;
            if(move){
                Debug.Log("Changeing direction");
                direction = Random.Range(0, 2) * 2 - 1;
                spriteRenderer.flipX = direction < 0;
            }
            // Print the current state of 'move' for debugging
            Debug.Log("Move is now: " + move);

            // Wait for a random duration between 1 and 3 seconds
            float waitTime = Random.Range(2f, 5f);
            yield return new WaitForSeconds(waitTime);

        }
    }
}
