using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; // Make sure to include this for Tilemap access

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your enemy prefab in the Inspector
    public float spawnInterval = 2f; // Time between spawns
    public Tilemap wallTilemap; // Assign your wall tilemap in the Inspector

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition;

        for (int i = 0; i < 10; i++) // Try up to 10 times to find a valid position
        {
            // Generate a random position within the tilemap bounds
            Vector2 randomPosition = new Vector2(
                Random.Range(wallTilemap.cellBounds.min.x, wallTilemap.cellBounds.max.x),
                Random.Range(wallTilemap.cellBounds.min.y, wallTilemap.cellBounds.max.y)
            );

            // Snap the position to the nearest cell center
            Vector3Int cellPosition = wallTilemap.WorldToCell(randomPosition);
            spawnPosition = wallTilemap.GetCellCenterWorld(cellPosition);

            // Check if spawnPosition is within the wall tilemap's empty space
            if (!IsPositionInsideWall(spawnPosition))
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                return; // Exit once a valid spawn position is found
            }
        }

        Debug.LogWarning("Failed to find a valid spawn position within walls.");
    }


    private bool IsPositionInsideWall(Vector2 position)
    {
        // Convert world position to tilemap cell position
        Vector3Int cellPosition = wallTilemap.WorldToCell(position);

        // Check if there's a tile at this cell position
        return wallTilemap.HasTile(cellPosition);
    }
}
