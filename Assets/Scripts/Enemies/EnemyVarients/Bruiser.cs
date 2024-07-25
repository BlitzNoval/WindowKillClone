using System.Collections;
using UnityEngine;

public class Bruiser : MonoBehaviour
{
    public float moveSpeed = 2.0f;          // Speed of the Brute during patrol
    public float chargeSpeed = 5.0f;       // Speed of the Brute during charge
    public float chargeCooldown = 5.0f;    // Cooldown time between charges
    public float detectionRange = 10.0f;   // Range at which the Brute detects the player
    public float chargeRange = 5.0f;       // Range at which the Brute starts charging
    public float patrolRadius = 5.0f;      // Radius of the patrol area
    public int health = 20;                // Health of the Brute
    public GameObject dropObject;          // Object to be dropped on death
    [Range(0, 100)] public float dropRate = 100f; // Drop rate in percentage

    private Transform player;              // Reference to the player's transform
    private PlayerResources playerResources; // Reference to PlayerResources script
    private Vector3 patrolCenter;          // Center of the patrol area
    private Vector3 patrolTarget;          // Current patrol target
    private bool isCharging = false;       // Flag to check if the Brute is charging
    private bool isPatrolling = true;      // Flag to check if the Brute is patrolling

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerResources = PlayerResources.Instance;

        patrolCenter = transform.position; // Set patrol center to the Brute's starting position
        SetNewPatrolTarget();

        StartCoroutine(ChargeCooldown());
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                if (distanceToPlayer <= chargeRange && !isCharging)
                {
                    // Start charging towards the player
                    StartCoroutine(ChargeTowardsPlayer());
                }
                else if (!isCharging)
                {
                    // Chase the player
                    MoveTowardsPlayer();
                }
            }
            else if (isPatrolling)
            {
                // Continue patrolling
                Patrol();
            }
        }

        // Check Brute health
        if (health <= 0)
        {
            Die();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolTarget) < 0.2f)
        {
            SetNewPatrolTarget();
        }
        else
        {
            Vector3 direction = (patrolTarget - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void SetNewPatrolTarget()
    {
        patrolTarget = patrolCenter + new Vector3(
            Random.Range(-patrolRadius, patrolRadius),
            Random.Range(-patrolRadius, patrolRadius),
            0
        );
    }

    private IEnumerator ChargeTowardsPlayer()
    {
        isCharging = true;
        isPatrolling = false;

        Vector3 chargeDirection = (player.position - transform.position).normalized;
        float chargeDuration = Vector3.Distance(transform.position, player.position) / chargeSpeed;

        float elapsed = 0f;
        while (elapsed < chargeDuration)
        {
            transform.position += chargeDirection * chargeSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        isCharging = false;
        isPatrolling = true;
    }

    private IEnumerator ChargeCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(chargeCooldown);

            // Make sure charging can be done again after cooldown
            isCharging = false;
            isPatrolling = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int playerHealthBefore = playerResources.health;
            playerResources.DamagePlayer(2); // Use the Brute's damage value

            // Log to the console
            Debug.Log($"Player hit by Brute. Player health before: {playerHealthBefore}, after: {playerResources.health}");

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
        // Check if the Brute should drop an object
        if (dropObject != null && Random.value <= dropRate / 100f)
        {
            Instantiate(dropObject, transform.position, Quaternion.identity);
        }

        // Destroy the Brute
        Destroy(gameObject);
    }
}
