using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim; 
    private SpriteRenderer spriteRenderer;
    // the amount of time is takes for the plant to spawn a new enemy 
    public PlantProduction plantProduction; 
    public EnemySpawner enemySpawner; 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemySpawner = GetComponent<EnemySpawner>();
        plantProduction = GetComponent<PlantProduction>();

        StartCoroutine(PlaySpawnAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        updateAnimationState();
    }

    public void updateAnimationState()
    {
        // anim.Play("PlantSpawn");
        if (plantProduction != null && plantProduction.readyForHarvest) anim.SetInteger("state",2);
        else anim.SetInteger("state",1);
    }

    // todo: fix glitch in spawning transition
    private IEnumerator PlaySpawnAnimation()
    {
        Debug.Log("Playing spawn animation");

        // yield return new WaitForSeconds(1);
        // Wait until the spawn animation is playing
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("PlantSpawn"));

        // Now wait for the spawn animation to complete
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9);


        anim.SetInteger("state",1);

        // Spawn animation has finished, you can perform additional actions here if necessary
    }

}
