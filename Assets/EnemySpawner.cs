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

    public int spawnInterval; 
    void Start()
    {
        GameObject gridGameObject = GameObject.Find("Grid");
        tilemap = gridGameObject.transform.Find("ContactMap").GetComponent<Tilemap>();   
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemyNearby()
    {
        Vector3 spawnPosition = GetRandomPositionInNearbyNonFilledTile();
        

        if(spawnPosition != Vector3.zero) // Check if a valid position was found
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("No valid position found for spawning");
        }
    }
       public Vector3 GetRandomPositionInNearbyNonFilledTile()
    {
        Vector3Int currentTile = tilemap.WorldToCell(transform.position);
        List<Vector3Int> nonFilledTiles = FindNonFilledHorizontalAdjacentTiles(currentTile);

        if (nonFilledTiles.Count == 0)
        {
            Debug.LogError("No non-filled tiles found above or below");
            return Vector3.zero;
        }

        Vector3Int selectedTile = nonFilledTiles[Random.Range(0, nonFilledTiles.Count)];
        return GetRandomPositionInTile(selectedTile);
    }

    private List<Vector3Int> FindNonFilledHorizontalAdjacentTiles(Vector3Int currentTile)
    {
        List<Vector3Int> nonFilledTiles = new List<Vector3Int>();

        // Check only the tiles above and below
        for (int x = -1; x <= 1; x++)
        {
            if (x == 0) continue; // Skip the current tile

            Vector3Int checkingTile = new Vector3Int(currentTile.x+x, currentTile.y , currentTile.z);
            if (!tilemap.HasTile(checkingTile))
            {
                nonFilledTiles.Add(checkingTile);
            }
        }

        return nonFilledTiles;
    }

    private Vector3 GetRandomPositionInTile(Vector3Int tile)
    {
        Vector3 tileCenter = tilemap.GetCellCenterWorld(tile);
        Vector3 tileSize = tilemap.cellSize;

        float randomX = Random.Range(tileCenter.x - tileSize.x / 2, tileCenter.x + tileSize.x / 2);
        float randomY = tileCenter.y - tileSize.y / 2;

        return new Vector3(randomX, randomY, tileCenter.z);
    }

    private IEnumerator SpawnRoutine()
    {
        while (true) // Infinite loop to keep the coroutine running
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval
            SpawnEnemyNearby(); // Call the spawn function

        }
    }
}


