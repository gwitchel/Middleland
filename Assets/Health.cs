using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : EnemyMovement
{
    [SerializeField] public int health = 5; 

    private int MAX_HEALTH = 6; 

    public float dieTime; 
    public float damageTime; 

    private Animator anim; 
    public Rigidbody2D rb;

    void Start(){
        anim = GetComponent<Animator>();
        rb =  GetComponent<Rigidbody2D>();
    }

    public HealthDisplay healthDisplay; 
    public void Damage(int amount){
        if(amount > 0)
        {
            this.health -= amount;
            Damage(); 
        }
        if (health <= 0)
        {
            Die();
        }

        if(healthDisplay != null)
        {
            healthDisplay.updateHealthDisplay();
        }
        
        
    }
    public void Heal(int amount){
         if(amount > 0)
        {
            this.health = Mathf.Min(this.health+amount,MAX_HEALTH);
        }
    }
    private void Die(){
        StartCoroutine(DieCoroutine());;
    }
    private IEnumerator DieCoroutine()
    {
        if (anim)
        {
            yield return StartCoroutine(DamageCoroutine());
            yield return new WaitForSeconds(0.5f);
            anim.SetLayerWeight(anim.GetLayerIndex("Die"), 1);
            yield return new WaitForSeconds(dieTime);
        }
        
        Destroy(gameObject);
    }
    private void Damage(){
        StartCoroutine(DamageCoroutine());
    }
    private IEnumerator DamageCoroutine()
    {
        if (anim)
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Damage"), 1);
            yield return new WaitForSeconds(damageTime);
            anim.SetLayerWeight(anim.GetLayerIndex("Damage"), 0);
        }
    }
}
