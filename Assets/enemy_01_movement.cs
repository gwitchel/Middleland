using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class enemy_01_movement : EnemyMovement
{
    public Tilemap tilemap;
    private float speed = 2f;
    private bool move = false;
    public Rigidbody2D rb;
    private Animator anim; 
    private SpriteRenderer spriteRenderer;
    private GameObject protagonist; 
    public bool begunDamage = false; 
    private float direction = 1;
    public GameObject attackArea;

    private BoxCollider2D BoxCollider2DBody;
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
        protagonist =  GameObject.Find("/Protagonist");
        BoxCollider2DBody = GetComponent<BoxCollider2D>();

        GameObject gridGameObject = GameObject.Find("Grid");
        // tilemap = gridGameObject.transform.Find("test_map_contact").GetComponent<Tilemap>();  
        tilemap = gridGameObject.transform.Find("ContactMap").GetComponent<Tilemap>();  

        StartCoroutine(PlaySpawnAnimation());
        StartCoroutine(ToggleMoveRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetInteger("state") > 0 ) updateAnimationState();
    }
    public void updateAnimationState(){
        if (anim.GetLayerWeight(anim.GetLayerIndex("Damage")) == 1 && !begunDamage)
        {
            begunDamage = true;
            DamageBounce(); 
        }
        else if (anim.GetLayerWeight(anim.GetLayerIndex("Damage")) != 1 && begunDamage)
        {
            begunDamage = false; 
        }

        else if (anim.GetLayerWeight(anim.GetLayerIndex("Damage")) == 0) 
        {
            if(move)
            {
                Move();
            }
            else
            {
                anim.SetInteger("state",1);
                anim.Play("enemy_01_idle");
                rb.velocity = new Vector2(0,0);
            }
        }
    }
    public void DamageBounce()
    {
        if((protagonist.transform.position-this.transform.position).x>0) 
        {
            // positive: attacker is to right (bounce left )
            rb.velocity = new Vector2(-5,10);
        }
        else
        {
            // negative: attacker is to left (bounce right )
            rb.velocity = new Vector2(rb.velocity.x+5,rb.velocity.y+10);
        } 
    }

    private bool CanMoveToDirection( )
    {
        Debug.Log("Can move");
        Vector3 directionVec = new Vector3 (direction * speed, 0,0);
        Vector3Int targetCell = tilemap.WorldToCell(transform.position + directionVec * Time.deltaTime + new Vector3 (0, -1,0));
        return tilemap.HasTile(targetCell); // Returns true if the target cell is filled
    }
    
    public void Move()
    {
        anim.SetInteger("state",2);
        anim.Play("enemy_01_walk");
        // Randomly select a direction: 1 for right, -1 for left

        // Move the GameObject
        if(CanMoveToDirection())
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        } 
        else
        {
            direction *= -1;
            spriteRenderer.flipX = direction < 0;
        }  
    }

    private IEnumerator ToggleMoveRoutine()
    {
        while (true)
        {
            // Toggle the 'move' boolean
            move = !move;

            if(move)
            {
                direction = Random.Range(0, 2) * 2 - 1;
                spriteRenderer.flipX = direction < 0;
            }
 
            // Wait for a random duration between 1 and 3 seconds
            float waitTime = Random.Range(2f, 5f);
            yield return new WaitForSeconds(waitTime);

        }
    }

    private IEnumerator PlaySpawnAnimation()
    {
        rb.isKinematic = true;
        yield return new WaitForSeconds(3);
        anim.SetInteger("state",1);
        rb.isKinematic = false;
        BoxCollider2DBody.enabled = true;
        if (attackArea == null)
        {
            Debug.LogError("Attack area not found!");
        }
        else
        {
            // Initially disable the attack area
            attackArea.SetActive(true);
        }
    }
}
