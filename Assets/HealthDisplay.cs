using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private ProtagonistHealth protagonistHealth;
    public GameObject protagonist;
    public Rigidbody2D protagonistRB; 
    public GameObject Heart;
    public GameObject HalfHeart;

    public GameObject Reload;
    void Start()
    {
        protagonist =  GameObject.Find("Protagonist");
        protagonistHealth = protagonist.GetComponent<ProtagonistHealth>(); 
        protagonistRB = protagonist.GetComponent<Rigidbody2D>();
        updateHealthDisplay();
    }

    public void updateHealthDisplay(){
        Vector2 padding = new Vector2(0.75f,-0.5f) ;
        Vector2 leftCorner = Camera.main.ViewportToWorldPoint(new Vector2(0,1));
        leftCorner += padding;
        
        // remove all hearts 
        foreach (Transform child in transform) Destroy(child.gameObject);
    

        // reinstantiate hearts 
        for(float i = 1f; i < protagonistHealth.health; i++)
        {
            GameObject newHeart = Instantiate(Heart, this.transform);
            newHeart.transform.position = leftCorner;
            leftCorner += new Vector2(0.5f,0f);
        }
        
        if(protagonistHealth.health < protagonistHealth.MAX_HEALTH)
        {
            GameObject reload = Instantiate(Reload, this.transform);
            reload.transform.position = leftCorner;
        }
        
        
        
        // // prop half hearts 
        //     if (protagonistHealth.health % 2 == 1)
        // {
        //     GameObject newHeart = Instantiate(HalfHeart, this.transform);
        //     newHeart.transform.position = leftCorner + new Vector2((protagonistHealth.health-1)/3,0);
        // }
    }

}
