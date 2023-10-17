using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_area : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage = 3; 

    public Rigidbody2D rb; 

    private Animator anim; 

    private SpriteRenderer spriteRenderer;

    private player_movement protagonist_movement; 
    void Start(){
        rb =  this.transform.parent.GetComponent<Rigidbody2D>();
        anim = this.transform.parent.GetComponent<Animator>();
        spriteRenderer = this.transform.parent.GetComponent<SpriteRenderer>();
        protagonist_movement = this.transform.parent.GetComponent<player_movement>();
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.GetComponent<Health>() != null )
        {
            protagonist_movement.AttackObject();
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
        }
    }
}
