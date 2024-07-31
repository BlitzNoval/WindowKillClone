using System.Collections;
using UnityEngine;

public class Colossus : Enemy
{
    public float initialMoveSpeed = 2.0f; // Initial speed at which the Colossus moves
    public float moveSpeedMultiplier = 2.0f; // Multiplier for stats in the enhanced phase
    public float stopDistance = 5.0f;
    public float attackRange = 10.0f;
    public GameObject projectilePrefab;
    public float fireRate = 6.0f;
    public Transform[] firePoints; // Array of fire points
    public float warningDuration = 0.25f; // Duration of each warning flash
    public int warningFlashes = 2; // Number of warning flashes
    public float phaseDuration = 30.0f; // Duration before phase change

    private Color originalColor;
    private Vector3 targetPosition;
    private float wanderTime = 2.0f; // Time to decide on a new direction
    private float phaseTimer;
    private bool isEnhanced = false;

    protected override void Start()
    {
        base.Start();
        originalColor = spriteRenderer.color;
        targetPosition = transform.position;
        phaseTimer = phaseDuration;
        StartCoroutine(FireProjectiles());
        ChooseNewTargetPosition(); // Initialize the first target position
    }

    protected override void Update()
    {
        base.Update();

        phaseTimer -= Time.deltaTime;

        if (phaseTimer <= 0)
        {
            ActivateEnhancedPhase();
            phaseTimer = phaseDuration; // Reset phase timer if needed
        }

        MoveTowardsTarget();
    }

    private void ActivateEnhancedPhase()
    {
        if (!isEnhanced)
        {
            initialMoveSpeed *= moveSpeedMultiplier; // Double the movement speed
            fireRate /= moveSpeedMultiplier; // Adjust fire rate accordingly
            isEnhanced = true;
        }
    }

    private void ChooseNewTargetPosition()
    {
        // Choose a new target position within a certain range
        targetPosition = transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
    }

    private void MoveTowardsTarget()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, initialMoveSpeed * Time.deltaTime);

        // Check if the Colossus has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            ChooseNewTargetPosition(); // Choose a new target position if the current one is reached
        }

        // Flip sprite based on movement direction
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
