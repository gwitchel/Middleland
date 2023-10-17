using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_01_animations : MonoBehaviour
{
    private float v0x = 3f;

    private int movementDuration = 30;
    private int timer = 0; 
    public Rigidbody2D rb;
    private Animator anim; 
    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        updateAnimationState();
    }

    public void updateAnimationState(){
        timer ++ ;
        Debug.Log(timer);
        if (timer > movementDuration)
        {
            Debug.Log("Hit timer limit!");
            anim.SetBool("walking",!anim.GetBool("walking"));
            timer = 0;
        }
        if (anim.GetBool("walking")){
            rb.velocity = new Vector2(v0x,rb.velocity.y);
        }
    }
}
