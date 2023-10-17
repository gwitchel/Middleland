using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protagonist_attack : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject attackArea = default;
    bool attacking = false; 
    float timeToAttack = 0.5f; 
    float timer = 0f; 

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        Debug.Log(attackArea.activeSelf);
        attackArea.SetActive(false);
        Debug.Log(attackArea.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) )
        {
            Attack();
        }
        if (attacking)
        {
            timer += Time.deltaTime;
        }
        
        if(timer >= timeToAttack)
        {
            attackArea.SetActive(false);
            attacking = false ;
            timer = 0f;
        } 
    } 

    private void Attack(){
        attacking = true; 
        attackArea.SetActive(true);
        Debug.Log(attackArea.activeSelf);
    }
}
