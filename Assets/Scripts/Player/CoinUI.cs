using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : MonoBehaviour
{

    public PlayerResources playerResources;

    public float range = 2.0f;
    public float moveSpeed = 45f;
    public Transform playerTransform;

    private void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
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

        MoveToPlayer();
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= range)
        {
            MoveToPlayer();
        }
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player Collided wt Material");
            Destroy(gameObject);
           // playerResources.experience++;
            playerResources.materials++;

        }
    }

}

