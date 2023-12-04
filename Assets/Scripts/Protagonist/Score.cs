using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] public int score = 0;
    [SerializeField] public int winningScore = 10;
    
    public Animator scoreAnimator;

    void Start()
    {
        scoreAnimator.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            scoreAnimator.speed = 1f;
            IncreaseScore(1);
        }
        UpdateAnimation();
    }

    public void IncreaseScore(int amount ){
        score += amount;
        
        if (score >= winningScore)
        {
            Debug.Log("Level complete");
        } 
    }

    private void UpdateAnimation()
    {
        AnimatorClipInfo[] clipInfo = scoreAnimator.GetCurrentAnimatorClipInfo(0);
        float animationProgress = (float) score / winningScore;

        if (clipInfo.Length > 0)
        {
            AnimationClip clip = clipInfo[0].clip;

            float frameRate = clip.frameRate;
            float currentTime = scoreAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime * clip.length;
            float currentFrame = Mathf.FloorToInt(currentTime * frameRate) % clip.length;

            if(currentFrame < animationProgress)
            {
                scoreAnimator.speed = 1f;
            }
            else 
            {
                scoreAnimator.speed = 0f;
            }
        }
    }
}
