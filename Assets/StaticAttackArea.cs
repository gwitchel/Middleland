using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAttackArea : MonoBehaviour
{
    // a static attack area for enemies to attack the protagonist
    public GameObject protagonist;
    public Rigidbody2D protagonistRB; 

    public Animator protagonistAnimator; 
    public ProtagonistHealth protagonistHealth; 

    public int damageAmount = 1;

    void Start()
    {
        protagonist =  GameObject.Find("Protagonist");
        protagonistRB = protagonist.GetComponent<Rigidbody2D>();
        protagonistHealth = protagonist.GetComponent<ProtagonistHealth>(); 
        protagonistAnimator = protagonist.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name=="Protagonist"){
            protagonistHealth.Damage(damageAmount,this.transform.parent.gameObject);
            protagonistAnimator.SetBool("damage",true);
        }
    }
}
