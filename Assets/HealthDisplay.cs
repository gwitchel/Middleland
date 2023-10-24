using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private Health protagonistHealth;
    public GameObject protagonist;
    public Rigidbody2D protagonistRB; 
    public GameObject Heart;
    public GameObject HalfHeart;

    void Start()
    {
        protagonist =  GameObject.Find("Protagonist");
        protagonistHealth = protagonist.GetComponent<Health>(); 
        Debug.Log(protagonistHealth.health);
        protagonistRB = protagonist.GetComponent<Rigidbody2D>();
        updateHealthDisplay();
    }

    public void updateHealthDisplay(){
        Vector2 padding = new Vector2(0.75f,-0.5f) ;
        Vector2 leftCorner = Camera.main.ViewportToWorldPoint(new Vector2(0,1));
        leftCorner += padding;
        // remove all hearts 
        
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        // repropogate hearts 
        for(float i = 0f; i < protagonistHealth.health; i++)
        {
            Debug.Log(i);
            GameObject newHeart = Instantiate(Heart, this.transform);
            newHeart.transform.position = leftCorner + new Vector2(i/3,0);
        }
        
        // // prop half hearts 
        //     if (protagonistHealth.health % 2 == 1)
        // {
        //     GameObject newHeart = Instantiate(HalfHeart, this.transform);
        //     newHeart.transform.position = leftCorner + new Vector2((protagonistHealth.health-1)/3,0);
        // }
    }
}
