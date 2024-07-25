using System.Collections;
using UnityEngine;

public class Spitter : MonoBehaviour
{
    public float moveSpeed = 3.0f;       // Speed of the Spitter
    public float runAwaySpeed = 5.0f;    // Speed of the Spitter when running away
    public float stopDistance = 5.0f;    // Distance at which the Spitter stops moving towards the player
    public float runAwayDistance = 3.0f; // Distance at which the Spitter starts running away from the player
    public float attackRange = 10.0f;    // Range at which the Spitter can attack the player
    public GameObject projectilePrefab;  // Projectile prefab
    public float fireRate = 6.0f;        // Rate at which the Spitter fires projectiles
    public Transform firePoint;          // Point from where the projectile is fired

    public int health = 8;               // Health of the Spitter
    public GameObject dropObject;        // Object to be dropped on death
    [Range(0, 100)] public float dropRate = 100f; // Drop rate in percentage

    private Transform player;            // Reference to the player's transform
    private PlayerResources playerResources; // Reference to PlayerResources script
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        // Find the player and get the PlayerResources component
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerResources = PlayerResources.Instance;

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start the firing coroutine
        StartCoroutine(FireProjectiles());
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Move towards the player until within stopDistance
            if (distanceToPlayer > stopDistance)
            {
                MoveTowardsPlayer();
            }
            else
            {
                // If within stopDistance, stop and attack the player
                // This is handled by the firing coroutine
            }

            // If the player moves within the runAwayDistance, run away
            if (distanceToPlayer < runAwayDistance)
            {
                RunAwayFromPlayer();
            }
        }

        // Check Spitter health
        if (health <= 0)
        {
            Die();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Flip the sprite based on movement direction
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void RunAwayFromPlayer()
    {
        Vector3 direction = (transform.position - player.position).normalized;
        transform.position += direction * runAwaySpeed * Time.deltaTime;

        // Flip the sprite based on movement direction
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private IEnumerator FireProjectiles()
    {
        while (true)
        {
            // Fire a projectile
            if (projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                projectile.GetComponent<EnemyProjectile>().Initialize(player.position - firePoint.position);
            }

            // Wait for the next fire time
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the Spitter collides with the player, deal damage
        if (collision.gameObject.CompareTag("Player"))
        {
            int playerHealthBefore = playerResources.health;
            playerResources.DamagePlayer(1); // Use the Spitter's damage value

            // Log to the console
            Debug.Log($"Player hit by Spitter. Player health before: {playerHealthBefore}, after: {playerResources.health}");

            // Destroy the player if health is below or equal to 0
            if (playerResources.health <= 0)
            {
                Debug.Log("Player has died.");
                Destroy(collision.gameObject);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        // Placeholder for taking damage logic
        health -= amount;
    }

    private void Die()
    {
        // Check if the Spitter should drop an object
        if (dropObject != null && Random.value <= dropRate / 100f)
        {
            Instantiate(dropObject, transform.position, Quaternion.identity);
        }

        // Destroy the Spitter
        Destroy(gameObject);
    }
}
