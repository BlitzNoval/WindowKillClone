using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public float damage;
    public float hpIncreasePerWave;
    public float damageIncreasePerWave;

    protected virtual void Start()
    {
        // Initialize enemy stats or other common logic
    }

    protected virtual void Update()
    {
        // Common update logic for all enemies
    }

    public abstract void PerformBehavior();
}
