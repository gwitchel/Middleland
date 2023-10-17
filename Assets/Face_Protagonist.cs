using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face_Protagonist : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject protagonist;

    public Vector2 protagonistPos; 

    public Rigidbody2D rb; 

    public  Vector2 pos; 

    private SpriteRenderer spriteRenderer;
    void Start()
    {
       protagonist = GameObject.Find("Protagonist");  
       rb =  GetComponent<Rigidbody2D>();
       spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        protagonistPos = protagonist.transform.position;
        pos = rb.transform.position;
        
        // protagonist is to left of object and object is facing right 
        if(pos.x-protagonistPos.x > 0 && !spriteRenderer.flipX) spriteRenderer.flipX = true;
        
        // protagonist is to left of object and object is facing right 
        if(pos.x-protagonistPos.x < 0 && spriteRenderer.flipX) spriteRenderer.flipX = false;
    }
}
