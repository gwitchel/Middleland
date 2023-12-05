using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class LevelLoader : MonoBehaviour
{
   public Animator anim; 
   public Animator protagonistAnimator;
   public float transitionTime = 10f; 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int LevelIndex){
        yield return StartCoroutine(PlayDieAnimation());
        anim.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(LevelIndex);
    }

    private IEnumerator PlayDieAnimation()
    {
        
        // Set the Damage layer weight to 1
        protagonistAnimator.SetLayerWeight(protagonistAnimator.GetLayerIndex("damage"), 1);
        protagonistAnimator.SetBool("die",true);

        // Wait for 3 seconds
        yield return new WaitForSeconds(1);

        // Reset the Damage layer weight
        // protagonistAnimator.SetLayerWeight(protagonistAnimator.GetLayerIndex("damage"), 0);
        // protagonistAnimator.SetBool("die",false);
    }
}
