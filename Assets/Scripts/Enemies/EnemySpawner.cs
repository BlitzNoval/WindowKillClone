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

            for (int i = 0; i < spawnAmount; i++)
            {
                // Get the bounds of the playable area
                Vector3 spawnPosition = new Vector3(
                    Random.Range(playableAreaBounds.min.x, playableAreaBounds.max.x),
                    Random.Range(playableAreaBounds.min.y, playableAreaBounds.max.y),
                    0
                );

                GameObject spawnedIndicator = Instantiate(spawnIndicatorPrefab, spawnPosition, transform.rotation);
                spawnedIndicator.GetComponent<EnemySpawnIndicator>().enemyToSpawn = enemiesToSpawn[randEnemy];
            } 
        }
    }
}
