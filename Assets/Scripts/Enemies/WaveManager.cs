using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timerText;
    public int totalWaves = 20;
    public float timeBetweenWaves = 10f;

    private int currentWave = 1;
    private float waveTimer = 0f;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        waveTimer += Time.deltaTime;
        if (waveTimer >= timeBetweenWaves)
        {
            if (currentWave < totalWaves)
            {
                currentWave++;
                StartWave();
                waveTimer = 0f;
            }
        }
        UpdateUI();
    }

    void StartWave()
    {
        enemySpawner.ResetSpawner(currentWave * 5); // Example scaling
    }

    void UpdateUI()
    {
        waveText.text = "Wave: " + currentWave + "/" + totalWaves;
        timerText.text = "Next Wave In: " + (timeBetweenWaves - waveTimer).ToString("F2") + "s";
    }
}
