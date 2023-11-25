using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 
public class Planting : MonoBehaviour
{
    public Tilemap tilemap; // Assign this in the inspector or find it dynamically
    private GameObject protagonist;

    private SpriteRenderer spriteRenderer;

    public GameObject BasicPlant; 
    void Start()
    {
        // Find the Protagonist GameObject
        protagonist = GameObject.Find("Protagonist");
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
        if(canPlant() && Input.GetKeyDown(KeyCode.P))
        {
            Plant();
        }
        
    }

    public bool canPlant()
    {
        if (protagonist != null && tilemap != null)
        {
            Vector3Int tilePosition = spriteRenderer.flipX ? tilemap.WorldToCell(protagonist.transform.position) - new Vector3Int(1, 1,0) : tilemap.WorldToCell(protagonist.transform.position) - new Vector3Int(-1, 1,0);
            // if(spriteRenderer.flipX) Vector3Int tilePosition = tilemap.WorldToCell(protagonist.transform.position) - new Vector3Int(-1, 1,0);
            

            // Check if there's a tile at this position
            if (tilemap.HasTile(tilePosition))
            {
                return true; 
            }
        }

        return false; 

    }

    public void Plant(){
        Debug.Log(protagonist.transform.position);
        Vector3Int protagonistTilePosition = tilemap.WorldToCell(protagonist.transform.position);
        Debug.Log(protagonistTilePosition);
        // Calculate the position of the tile to the left (-1 on the x-axis)
        Vector3 spawnPosition = tilemap.CellToWorld(new Vector3Int(protagonistTilePosition.x - 1, protagonistTilePosition.y+1, protagonistTilePosition.z));
        // Vector2 spawnPosition = spriteRenderer.flipX ? protagonist.transform.position + new Vector3(-1,0.3f,0) : protagonist.transform.position + new Vector3(1,0.3f,0);
        // tilemap.CellToWorld(tilemapCoordinates);
        Debug.Log(spawnPosition);
        Instantiate(BasicPlant, spawnPosition, Quaternion.identity);
    }
}