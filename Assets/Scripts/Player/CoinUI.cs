using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public PlayerResources playerResources;
    public WaveSpawner waveSpawner;

    private float range = 8.0f; // Range within which the coin will move towards the player
    public float moveSpeed = 35f; // Speed at which the coin moves towards the player
    public Transform playerTransform;

    private void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
        waveSpawner = FindAnyObjectByType<WaveSpawner>();
        if (playerResources == null)
        {
            Debug.LogError("PlayerResources script not found in the scene.");
        }
        else
        {
            playerTransform = playerResources.transform;
        }
    }

    private void Update()
    {
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= range)
        {
            MoveToPlayer();
         
        }
    }

    void MoveToPlayer()
    {

        Debug.Log("Move to player called coin is in range");
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }

    public void MoveToBag()
    {

        CoinUI[] allCoins = FindObjectsOfType<CoinUI>();
        int coinCount = allCoins.Length;
        Debug.Log("Number of coins in the scene: " + coinCount);
        LubaUI lubaUi = FindAnyObjectByType<LubaUI>();
        playerResources.baggedMaterials += coinCount;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Collided with Material");
            Destroy(gameObject);

            playerResources.GainExperience(1);
        }
    }
}
