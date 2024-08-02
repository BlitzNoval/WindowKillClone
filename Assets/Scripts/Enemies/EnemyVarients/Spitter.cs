using System.Collections;
using UnityEngine;

public class Spitter : Enemy
{
    public float moveSpeed = 2.0f; // Speed at which the Spitter floats around
    public float stopDistance = 5.0f;
    public float attackRange = 10.0f;
    public GameObject projectilePrefab;
    public float fireRate = 6.0f;
    public Transform[] firePoints; // Array of fire points
    public float warningDuration = 0.25f; // Duration of each warning flash
    public int warningFlashes = 2; // Number of warning flashes

    private Color originalColor;
    private Vector3 targetPosition;
    private float wanderTime = 2.0f; // Time to wander before choosing a new direction
    private float wanderTimer;

    protected override void Start()
    {
        base.Start();
        originalColor = spriteRenderer.color;
        targetPosition = transform.position;
        wanderTimer = wanderTime;
        StartCoroutine(FireProjectiles());
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            Wander();
        }

        // Optionally, you can add logic here if you want the Spitter to do something specific when close to the player
    }

    private void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            // Choose a new target position within a certain range
            targetPosition = transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            wanderTimer = wanderTime;
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        FlipSprite((targetPosition - transform.position).normalized);
    }

    private IEnumerator FireProjectiles()
    {
        while (true)
        {
            // Flash red as a warning before firing
            for (int i = 0; i < warningFlashes; i++)
            {
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(warningDuration);
                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(warningDuration);
            }

            // Fire projectiles from each fire point
            if (projectilePrefab != null && firePoints.Length > 0)
            {
                foreach (Transform firePoint in firePoints)
                {
                    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                    projectile.GetComponent<EnemyProjectile>().Initialize(player.position - firePoint.position);
                }
            }

            yield return new WaitForSeconds(fireRate);
        }
    }
}
