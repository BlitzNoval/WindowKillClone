using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemiesPerWave = 5;
    public float spawnInterval = 2f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public float cornerOffset = 1f;
    public Transform spawnPointsParent;

    private float timer = 0f;
    private int spawnedEnemies = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval && spawnedEnemies < enemiesPerWave)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition;
        bool validPosition = false;

        while (!validPosition)
        {
            spawnPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            if (IsValidSpawnPosition(spawnPosition))
            {
                validPosition = true;
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, spawnPointsParent);
                spawnedEnemies++;
            }
        }
    }

    bool IsValidSpawnPosition(Vector2 position)
    {
        if (position.x < spawnAreaMin.x + cornerOffset || position.x > spawnAreaMax.x - cornerOffset ||
            position.y < spawnAreaMin.y + cornerOffset || position.y > spawnAreaMax.y - cornerOffset)
        {
            return false;
        }
        return true;
    }

    public void ResetSpawner(int newEnemiesPerWave)
    {
        spawnedEnemies = 0;
        enemiesPerWave = newEnemiesPerWave;
    }
}
