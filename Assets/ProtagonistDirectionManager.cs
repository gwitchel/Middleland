using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionManager : MonoBehaviour
{
    // Start is called before the first frame update

    // a global class to manage the direction the protagonist is looking 
    private Animator anim; 
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetLayerWeight(anim.GetLayerIndex("down"), 1f);
        }
        else 
        {
            anim.SetLayerWeight(anim.GetLayerIndex("down"), 0f);
        }
    }

}
