using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class Planting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) )
        {
            Plant();
        }
    }


    public void Plant()
    {

    }
    // IEnumerator AddObject()
    // {
    //     // Instantiate(sampleObject, Vector3.zero, Quaternion.Identity);
    // }
}
