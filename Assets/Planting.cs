using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// planting code is trash and cuases a bunch of warning/errors. Needs to be fixed
public class Planting : MonoBehaviour
{
    public Tilemap tilemap; // Assign this in the inspector or find it dynamically
    private GameObject protagonist;
    private Rigidbody2D protagonistRB;
    private SpriteRenderer spriteRenderer;
    public GameObject BasicPlant; 
    public GameObject GhostPlant; 
     public Animator anim; 
    private bool GhostPlaced = false; 
    public Tile DiggableTile;
    void Start()
    {
        // Find the Protagonist GameObject
        protagonist = GameObject.Find("Protagonist");
        protagonistRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        if (protagonist == null)
        {
            Debug.LogError("Protagonist GameObject not found.");
        }

        // Optionally find the Tilemap dynamically if not set in the inspector
        if (tilemap == null)
        {
            tilemap = FindObjectOfType<Tilemap>();
            if (tilemap == null)
            {
                Debug.LogError("Tilemap not found.");
            }
        }
    }

    void Update()
    {
        // if the protagonist is moves destroy the ghost 
        if (Mathf.Abs(protagonistRB.velocity.x) > 0.1f || Mathf.Abs(protagonistRB.velocity.x) > 0.1f)
        {
            GhostPlaced = false; 
            GameObject ghostPlant = GameObject.Find("ghostPlant(Clone)");
            Destroy(ghostPlant);
        }
        if(canPlant() && !GhostPlaced && Input.GetKeyDown(KeyCode.P))
        {
            DisplayGhost(); 
        } 
        else if(canPlant() && GhostPlaced && Input.GetKeyDown(KeyCode.P))
        {
            ReplaceGhostPlantWithPlant();
        }
        else if (Input.GetKeyDown(KeyCode.D)){
            Dig();
        }
        else if (Input.GetKeyDown(KeyCode.H)){
            Harvest();
        }
        
    }

    public bool canPlant()
    {
        if (protagonist != null && tilemap != null)
        {
            Vector3Int protagonistTilePosition = tilemap.WorldToCell(protagonist.transform.position);
            Vector3Int tilePosition = spriteRenderer.flipX ? new Vector3Int(protagonistTilePosition.x - 1, protagonistTilePosition.y-1, protagonistTilePosition.z) : new Vector3Int(protagonistTilePosition.x + 2, protagonistTilePosition.y-1, protagonistTilePosition.z);
            // Check if there's plantable dirt at this position
            if (tilemap.HasTile(tilePosition))
            {
                return true; 
            }
        }

        return false; 

    }
    public void DisplayGhost(){
        // calculate spawn position on tilemap
        Vector3Int protagonistTilePosition = tilemap.WorldToCell(protagonist.transform.position);
        Vector3 spawnPosition = spriteRenderer.flipX ? tilemap.CellToWorld(new Vector3Int(protagonistTilePosition.x - 1, protagonistTilePosition.y, protagonistTilePosition.z)) : tilemap.CellToWorld(new Vector3Int(protagonistTilePosition.x + 2, protagonistTilePosition.y, protagonistTilePosition.z));

        // spawn a plant at the target position
        Instantiate(GhostPlant, spawnPosition, Quaternion.identity);
        GhostPlaced = true;

    }
    public void ReplaceGhostPlantWithPlant()
    {
        StartCoroutine(PlayPlantingAnimation());
        GameObject ghostPlant = GameObject.Find("ghostPlant(Clone)");
   
        Vector3Int ghostPlantTilePosition = tilemap.WorldToCell(ghostPlant.transform.position);
        Vector3Int blockBelowSpawnPosition =  new Vector3Int(ghostPlantTilePosition.x , ghostPlantTilePosition.y-1, ghostPlantTilePosition.z);


        // Instantiate the Plant prefab at the position and rotation of ghostPlant
        GameObject newPlant = Instantiate(BasicPlant, ghostPlant.transform.position, ghostPlant.transform.rotation);

        // Optionally, set the new plant's parent to ghostPlant's parent
        newPlant.transform.parent = ghostPlant.transform.parent;
        tilemap.SetTile(blockBelowSpawnPosition, null);

        // Destroy the ghostPlant
        Destroy(ghostPlant);
        GhostPlaced = false;
    }
    public GameObject GetSelectedPlant(){
        // TODO: Change 
        Vector2 checkAreaSize = new Vector2(1f, 1f);
        float checkDistance = 0.5f;
        // Calculate the position to check
        Vector2 checkPosition = spriteRenderer.flipX ? (Vector2)protagonist.transform.position - (Vector2)protagonist.transform.right * checkDistance :(Vector2)protagonist.transform.position + (Vector2)protagonist.transform.right * checkDistance;

        // Check for a plant in the defined area
        Collider2D[] colliders = Physics2D.OverlapBoxAll(checkPosition, checkAreaSize, protagonist.transform.eulerAngles.z);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.name == "Plant(Clone)")
            {
                return collider.gameObject;
            }
        }

        // Return null if no plant is found
        return null;
    }

    public void Dig(){
        GameObject selectedPlant = GetSelectedPlant();
        if(selectedPlant){
            Vector3Int plantTilePosition = tilemap.WorldToCell(selectedPlant.transform.position);
            Vector3Int TileBelowPlantTilePosition = new Vector3Int(plantTilePosition.x, plantTilePosition.y-1, plantTilePosition.z);
            
            tilemap.SetTile(TileBelowPlantTilePosition, DiggableTile);
            Destroy(selectedPlant);
        }

    }
    public void Harvest()
    {
        GameObject selectedPlant = GetSelectedPlant();
        PlantProduction plantProduction = selectedPlant.GetComponent<PlantProduction>();
        Debug.Log("Harvesting");
        if(plantProduction.readyForHarvest){
            plantProduction.Harvest(); 
        }
    }

    private IEnumerator PlayPlantingAnimation()
    {
        int layerIndex = anim.GetLayerIndex("Driven Events");
        anim.SetLayerWeight(layerIndex, 1f); // Activate the layer
        anim.Play("Plant");
        yield return new WaitForSeconds(0.45f); // Wait for the specified duration
        anim.SetLayerWeight(layerIndex, 0f); // Deactivate the layer
    }
}