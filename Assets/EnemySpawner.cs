using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Set this to your enemy prefab in the inspector
    public float averageRadius = 2f; // Average radius from the GameObject
    public float radiusVariance = 1f; // Variance in the radius
    public Tilemap tilemap;
    void Start()
    {
        GameObject gridGameObject = GameObject.Find("Grid");
        tilemap = gridGameObject.transform.Find("test_map_contact").GetComponent<Tilemap>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemyNearby()
    {
        Vector2 spawnPosition = CalculateSpawnPosition();

        if(spawnPosition != Vector2.zero) // Check if a valid position was found
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("No valid position found for spawning");
        }
    }
    Vector2 CalculateSpawnPosition()
    {
        for(int i = 0; i < 30; i++) // Try multiple times to find a valid position
        {

            // Calculate a random radius with more weight towards the average radius
            float randomRadius = averageRadius + Random.Range(-radiusVariance, radiusVariance) * Mathf.Pow(Random.value, 2);

            // Calculate the spawn position
            // Vector2 potentialSpawnPosition = new Vector2(transform.position.x,transform.position.y) + randomRadius * new Vector2(Random.value < 0.5f ? -1 : 1, 0);
            Vector2 potentialSpawnPosition = new Vector2(transform.position.x,transform.position.y) + randomRadius * new Vector2(Random.value < 0.5f ? -1 : 1, 0.75f);

            // Check if the position is valid (not overlapping and on the same surface)
            if(IsValidSpawnPosition(potentialSpawnPosition))
            {
                return potentialSpawnPosition;
            }
        }

        return Vector2.zero; // Return zero if no valid position was found
    }
    bool IsValidSpawnPosition(Vector2 position)
    {
        // Check for overlap with other colliders
        // if(Physics2D.OverlapPoint(position)) // Adjust the radius as needed for your game
        // {
        //     return false;
        // }

        return(IsPositionFilled(position));

        // Optionally, add additional checks here, like ensuring the enemy is on the same surface

        // return true;
    }
    public bool IsPositionFilled(Vector2 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(new Vector3(worldPosition.x,worldPosition.y,0)); // Convert world position to cell position
        return tilemap.GetTile(cellPosition) != null; // Check if there is a tile at this position
    }
    
        
}
