using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_01_movement : MonoBehaviour
{
 private float v0x = 1f;

    private int movementDuration = 1000;
    private int damageDuration = 200;
    private int damageTimer = 0; 
    private int timer = 0; 
    public Rigidbody2D rb;
    private Animator anim; 
    private SpriteRenderer spriteRenderer;

    private GameObject protagonist; 

    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
        protagonist =  GameObject.Find("/Protagonist");
    }

    // Update is called once per frame
    void Update()
    {
        updateAnimationState();
    }

    public void updateAnimationState(){
        timer ++;
        if (anim.GetInteger("state")== 5){
            TakeDamage();
            
        }
        else if (timer > movementDuration)
        {
            anim.SetBool("walking",!anim.GetBool("walking"));
            timer = 0;
        } else if (anim.GetBool("walking") && Mathf.Abs(rb.velocity.y)<0.01f)
        {
            if(spriteRenderer.flipX)
            {
                rb.velocity = new Vector2(-1*v0x,0);
            }
            else
            {
                rb.velocity = new Vector2(v0x,0);
            }
            
        }
        else if(rb.velocity.y > 0.01f) {
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y + rb.velocity.y * Physics2D.gravity.y * Time.deltaTime);
        }
        else 
        {
            rb.velocity = new Vector2(0,0);
        }

    }

    public void TakeDamage(){
        if (damageTimer == 0)
        {            
            // positive: attacker is to right (bounce left )
            if((protagonist.transform.position-this.transform.position).x>0) rb.velocity = new Vector2(rb.velocity.x-3,rb.velocity.y+10);
            // negative: attacker is to left (bounce right )
            else rb.velocity = new Vector2(rb.velocity.x+3,rb.velocity.y+10);
        }
        else if(damageTimer > damageDuration)
        {
            // might need to be changed with advanced character movement
            anim.SetInteger("state",0);
            rb.velocity = new Vector2(0,0);
            damageTimer = -1;
        } else { 
            anim.Play("enemy_01_damage");
        }
        damageTimer++; 
      
    }
}
