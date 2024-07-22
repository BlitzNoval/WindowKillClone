using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnIndicator : MonoBehaviour
{
    public float indicatorLength;
    private Animator anim;
    public GameObject enemyToSpawn;

    private void Start()
    {   
        anim = GetComponent<Animator>();
        anim.speed = indicatorLength;
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyToSpawn, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
