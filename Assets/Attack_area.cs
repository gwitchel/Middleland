using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_area : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage = 1; 

    public Rigidbody2D rb; 

    public Animator anim; 

    public SpriteRenderer spriteRenderer;

    public player_movement protagonist_movement; 

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.GetComponent<Health>() != null )
        {
            //TODO: protagonist can attack multiple at once
            protagonist_movement.AttackObject(collider);
            Health componentHealth = collider.GetComponent<Health>();
            componentHealth.Damage(damage);
        }
    }
}
