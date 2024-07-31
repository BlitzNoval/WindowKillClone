using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Wave
{
    public float duration;
    public List<WaveSpawnInfo> enemiesToSpawn;
}

[System.Serializable]
public class WaveSpawnInfo
{
    public GameObject enemyPrefab;
    public int amount;
}

public class WaveSpawner : MonoBehaviour
{
     public static WaveSpawner Instance { get; private set; }

    public List<Wave> waves;
    public EnemySpawner enemySpawner;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timerText;

    public int currentWaveIndex;
    private float waveTimer;
    private bool isWaveActive;

    public bool IsWaveActive => isWaveActive;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner not set!");
            return;
        }

        currentWaveIndex = 0;
        isWaveActive = false;
        UpdateWaveText();
        UpdateTimerText();
    }

    private void Update()
    {
        if (isWaveActive)
        {
            waveTimer -= Time.deltaTime;
            UpdateTimerText();

            if (waveTimer <= 0)
            {
                isWaveActive = false;
                WaveCompleted();
            }
        }
    }

    private const float statIncreasePerWave = 1.006f; // 0.6% increase per wave

    private IEnumerator SpawnEnemies(Wave wave)
    {
        // Get the total number of enemies to spawn
        int totalEnemies = 0;
        foreach (var spawnInfo in wave.enemiesToSpawn)
        {
            totalEnemies += spawnInfo.amount;
        }

        // Calculate the interval for spawning enemies
        float spawnInterval = wave.duration / totalEnemies;

        // Spawn the enemies
        foreach (var spawnInfo in wave.enemiesToSpawn)
        {
            for (int i = 0; i < spawnInfo.amount; i++)
            {
                GameObject enemyInstance = Instantiate(spawnInfo.enemyPrefab);
                Enemy enemyScript = enemyInstance.GetComponent<Enemy>();

                if (enemyScript != null)
                {
                    float multiplier = Mathf.Pow(statIncreasePerWave, currentWaveIndex);
                    enemyScript.ApplyStatMultiplier(multiplier);
                }

                if (spawnInfo.enemyPrefab.GetComponent<Chaser>() != null)
                {
                    enemySpawner.SpawnGroup(spawnInfo.enemyPrefab, 5);
                }
                else
                {
                    enemySpawner.SpawnEnemy(spawnInfo.enemyPrefab);
                }

                // Wait for the next spawn interval
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    public void StartNextWave()
    {
        if (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];
            waveTimer = currentWave.duration;
            isWaveActive = true;
            StartCoroutine(SpawnEnemies(currentWave));
            UpdateWaveText();
            currentWaveIndex++; // Move to the next wave
        }
        else
        {
            waveText.text = "All waves completed!";
            // Optionally, you could disable further wave spawning or handle end-of-game logic here
        }
    }

    private void WaveCompleted()
    {
        // Logic to handle wave completion
        GameManager.Instance.SwitchState(GameManager.Instance.upgradeState);
    }

    private void UpdateWaveText()
    {
        if (currentWaveIndex < waves.Count)
        {
            waveText.text = $"Wave: {currentWaveIndex + 1}";
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = $"{Mathf.CeilToInt(waveTimer)} sec";
    }
}
