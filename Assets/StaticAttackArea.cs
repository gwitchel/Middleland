using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttackArea : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject protagonist;
    public Rigidbody2D protagonistRB; 

    public Animator protagonistAnimator; 
    public Health protagonistHealth; 

    public int damageAmount = 1;

    void Start()
    {
        protagonist =  GameObject.Find("Protagonist");
        protagonistRB = protagonist.GetComponent<Rigidbody2D>();
        protagonistHealth = protagonist.GetComponent<Health>(); 
        protagonistAnimator = protagonist.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name=="Protagonist"){
            protagonistHealth.Damage(damageAmount);
            protagonistAnimator.SetBool("damage",true);
        }
    }
}
