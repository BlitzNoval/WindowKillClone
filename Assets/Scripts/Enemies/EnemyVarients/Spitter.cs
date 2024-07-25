using System.Collections;
using UnityEngine;

public class Spitter : Enemy
{
    public float runAwaySpeed = 5.0f;
    public float stopDistance = 5.0f;
    public float runAwayDistance = 3.0f;
    public float attackRange = 10.0f;
    public GameObject projectilePrefab;
    public float fireRate = 6.0f;
    public Transform firePoint;
    public float warningDuration = 0.25f; // Duration of each warning flash
    public int warningFlashes = 2; // Number of warning flashes

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        StartCoroutine(FireProjectiles());
    }

    protected override void Update()
    {
        base.Update();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer > stopDistance)
        {
            MoveTowardsPlayer();
        }
        if (distanceToPlayer < runAwayDistance)
        {
            RunAwayFromPlayer();
        }
    }

    private void RunAwayFromPlayer()
    {
        Vector3 direction = (transform.position - player.position).normalized;
        transform.position += direction * runAwaySpeed * Time.deltaTime;
        FlipSprite(direction);
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

            // Fire a projectile
            if (projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                projectile.GetComponent<EnemyProjectile>().Initialize(player.position - firePoint.position);
            }
            yield return new WaitForSeconds(fireRate);
        }
    }
}
