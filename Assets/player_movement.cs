using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class player_movement : MonoBehaviour
{
    private float v0y = 24f; 
    private float v0x = 4f;
    public Rigidbody2D rb;
    private Animator anim; 
    private GameObject attackArea;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackArea =  GameObject.Find("Protagonist/AttackArea");
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        updateAnimationState();
        executeAnimationState();
    }

    public int speedCounter = 0;
    private int attackTime = 50; 
    private int attackCounter = 0; 
    private void updateAnimationState(){
        if (anim.GetBool("damage"))
        {
            anim.SetInteger("state",5);
        } 
        else if (Input.GetKeyDown(KeyCode.A) || (anim.GetInteger("state") == 4 && attackCounter < attackTime ))
        {
           
            attackCounter ++;
            
            anim.SetInteger("state",4);
        } 
        else if (attackCounter == attackTime)
        {
            attackCounter=0;
            anim.SetInteger("state",0);
            anim.Play("protagonist_idle");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && anim.GetInteger("state") <= 1 )
        {
            // taking off
            rb.velocity = new Vector2(rb.velocity.x*2,v0y);
            anim.SetInteger("state",2);
        } 
        else if (rb.velocity.y > 0.1f)
        {
            // jumping 
            anim.SetInteger("state",2);
        }
        else if (rb.velocity.y < -0.1f)   
        {
            // falling
            anim.SetInteger("state",3); 
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            // walking
            anim.SetInteger("state",1);
        }
        else
        {
            anim.SetInteger("state",0);
        }
    }

    private int state; 

    private void executeAnimationState(){
        state = anim.GetInteger("state");

        if(state == 5)
        {
            TakeDamage();
        }
        else if (state == 4)
        {
            if(Mathf.Abs(rb.velocity.y)<0.1f)
            {
                rb.velocity =  new Vector2(0, rb.velocity.y);
            }
        
        }
        else if (state == 2 || state == 3){
            // jumping or falling: Gravity handels this one
        }
        else if (state == 1){
            //walking 
        } 
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) )
        {
            rb.velocity = new Vector2(v0x, rb.velocity.y);
            spriteRenderer.flipX = false;
        } 
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(-1*v0x, rb.velocity.y);
            spriteRenderer.flipX = true; 
        }
    }

    public void AttackObject(Collider2D obj)
    {
        // positive: attacker is to right (bounce left )
        if((obj.transform.position-this.transform.position).x>0) rb.velocity = new Vector2(rb.velocity.x-3,rb.velocity.y+10);
        // negative attack is to left (bounce right )
        else rb.velocity = new Vector2(rb.velocity.x+3,rb.velocity.y+10);
    }
    
    private int damageDuration = 100;
    private int damageTimer = 0; 
    
    public void TakeDamage(){
        anim.SetInteger("state",0);
        if (damageTimer == 0)
        {
           anim.Play("protagonist_damage");
           rb.velocity = new Vector2(-5f,5f);
        }
        damageTimer++; 
        if(damageTimer > damageDuration)
        {
            anim.SetBool("damage",false);
            rb.velocity = new Vector2(0,0);
            damageTimer = 0;
        } else { 
            anim.Play("protagonist_damage");
        }
      
    }

    private void Walk(){
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && anim.GetInteger("state")<3 && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.A) ))
        {
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) )
            {
                anim.SetInteger("state",1);
                rb.velocity = new Vector2(v0x, rb.velocity.y);
                spriteRenderer.flipX = false;
            } 
            else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetInteger("state",1);
                rb.velocity = new Vector2(-1*v0x, rb.velocity.y);
                spriteRenderer.flipX = true; 
            }
        } 
        else if (Mathf.Abs(rb.velocity.x) < v0x && Mathf.Abs(rb.velocity.y) < 0.01)
        {
            anim.SetInteger("state",0);
        }
    }

    private void Look(){
        
    }
}

