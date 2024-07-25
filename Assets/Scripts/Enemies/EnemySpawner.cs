using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnIndicatorPrefab;
    public GameObject[] enemiesToSpawn;
    public SpriteRenderer playableAreaSpriteRenderer; // Reference to the SpriteRenderer defining the playable area

    private Bounds playableAreaBounds;

    private void Start()
    {
        if (playableAreaSpriteRenderer == null)
        {
            Debug.LogError("Playable area sprite renderer not set!");
            return;
        }

        // Calculate the bounds based on the sprite renderer
        playableAreaBounds = playableAreaSpriteRenderer.bounds;
    }

    public void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(playableAreaBounds.min.x, playableAreaBounds.max.x),
            Random.Range(playableAreaBounds.min.y, playableAreaBounds.max.y),
            0
        );

        GameObject spawnedIndicator = Instantiate(spawnIndicatorPrefab, spawnPosition, transform.rotation);
        spawnedIndicator.GetComponent<EnemySpawnIndicator>().enemyToSpawn = enemyPrefab;
    }

    public void SpawnGroup(GameObject enemyPrefab, int groupSize)
    {
        Vector3 centralPosition = new Vector3(
            Random.Range(playableAreaBounds.min.x, playableAreaBounds.max.x),
            Random.Range(playableAreaBounds.min.y, playableAreaBounds.max.y),
            0
        );

        List<Vector3> pattern = GetRandomPattern(groupSize);

        foreach (Vector3 offset in pattern)
        {
            Vector3 spawnPosition = centralPosition + offset;
            GameObject spawnedIndicator = Instantiate(spawnIndicatorPrefab, spawnPosition, transform.rotation);
            spawnedIndicator.GetComponent<EnemySpawnIndicator>().enemyToSpawn = enemyPrefab;
        }
    }

    private List<Vector3> GetRandomPattern(int groupSize)
    {
        List<List<Vector3>> patterns = new List<List<Vector3>>()
        {
            new List<Vector3> { new Vector3(-1, 0, 0), new Vector3(2, 0, 0), new Vector3(0, -1, 0), new Vector3(0, 1, 0) },
            new List<Vector3> { new Vector3(-1, -1, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, 1, 0) },
            new List<Vector3> { new Vector3(-1, 0, 0), new Vector3(1, 0, 0), new Vector3(0, -1, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) },
            // Add more patterns as needed
        };

        int randPatternIndex = Random.Range(0, patterns.Count);
        return patterns[randPatternIndex];
    }
}
