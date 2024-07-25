using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnIndicatorPrefab;
    public GameObject[] enemiesToSpawn;
    public float spawnSpeed;
    public int spawnAmount;

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

        StartCoroutine(EnemySpawnLoop());
    }

    private IEnumerator EnemySpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnSpeed);

            int randEnemy = Random.Range(0, enemiesToSpawn.Length);
            GameObject enemyPrefab = enemiesToSpawn[randEnemy];

            if (enemyPrefab.GetComponent<Chaser>() != null)
            {
                SpawnGroup(enemyPrefab, 5);
            }
            else
            {
                SpawnEnemy(enemyPrefab);
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // Get the bounds of the playable area
        Vector3 spawnPosition = new Vector3(
            Random.Range(playableAreaBounds.min.x, playableAreaBounds.max.x),
            Random.Range(playableAreaBounds.min.y, playableAreaBounds.max.y),
            0
        );

        GameObject spawnedIndicator = Instantiate(spawnIndicatorPrefab, spawnPosition, transform.rotation);
        spawnedIndicator.GetComponent<EnemySpawnIndicator>().enemyToSpawn = enemyPrefab;
    }

    private void SpawnGroup(GameObject enemyPrefab, int groupSize)
    {
        // Central spawn position for the group
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
