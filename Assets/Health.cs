using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 5; 

    private int MAX_HEALTH = 100; 

    private Animator anim; 
    public Rigidbody2D rb;
    void Start(){
        anim = GetComponent<Animator>();
        rb =  GetComponent<Rigidbody2D>();
    }
    public void Damage(int amount){
        if(amount > 0)
        {
            this.health -= amount;
            if (anim != null){
                anim.SetInteger("state",5);
            } 
        }
        if (health <= 0)
        {
            // anim.SetTrigger("damage");
            Die();
        }
        
    }

    public void Heal(int amount){
         if(amount > 0)
        {
            this.health = Mathf.Min(this.health+amount,MAX_HEALTH);
        }
    }

    private void Die(){
        anim.SetTrigger("death");
        Destroy(gameObject,1f);
    }
}
