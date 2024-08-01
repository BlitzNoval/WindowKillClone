using System.Collections;
using UnityEngine;

public class Colossus : Enemy
{
    public float initialMoveSpeed = 2.0f;
    public float moveSpeedMultiplier = 2.0f;
    public float stopDistance = 5.0f;
    public float attackRange = 10.0f;
    public GameObject projectilePrefab;
    public float fireRate = 6.0f;
    public Transform[] firePoints;
    public float warningDuration = 0.25f;
    public int warningFlashes = 2;
    public float phaseDuration = 30.0f;

    private Color originalColor;
    private Vector3 targetPosition;
    public float wanderTime = 2.0f;
    private float phaseTimer;
    private int currentPhase = 1;

    protected override void Start()
    {
        base.Start();
        originalColor = spriteRenderer.color;
        targetPosition = transform.position;
        phaseTimer = phaseDuration;
        StartCoroutine(FireProjectiles());
        ChooseNewTargetPosition();
    }

    protected override void Update()
    {
        base.Update();

        phaseTimer -= Time.deltaTime;

        if (phaseTimer <= 0)
        {
            Vector3 targetPosition = Vector3.Distance(transform.position, player.position) <= attackRange
                ? player.position
                : wanderTarget;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentMoveSpeed * Time.deltaTime);
            FlipSprite((targetPosition - transform.position).normalized);

            if (Vector3.Distance(transform.position, wanderTarget) < 0.1f)
            {
                SetWanderTarget();
            }
        }
        else if (currentPhase == 3)
        {
            ActivatePhase3();
        }
    }

    private void ActivatePhase2()
    {
        initialMoveSpeed *= moveSpeedMultiplier;
        fireRate /= moveSpeedMultiplier;
        // Additional behavior for Phase 2
        StartCoroutine(EnhancedBehavior());
    }

    private void ActivatePhase3()
    {
        initialMoveSpeed *= moveSpeedMultiplier;
        fireRate /= moveSpeedMultiplier;
        // Additional behavior for Phase 3
        StartCoroutine(UnpredictableBehavior());
    }

    private void SetWanderTarget()
    {
        wanderTarget = transform.position + Random.insideUnitSphere * wanderRadius;
        wanderTarget.z = 0;
    }

    private IEnumerator ManagePhases()
    {
        while (true)
        {
            yield return new WaitForSeconds(phaseDuration);
            AdvancePhase();
            phaseTimer = phaseDuration;
        }

        MoveTowardsTarget();
    }

    private void AdvancePhase()
    {
        currentPhase++;

        if (currentPhase == 2)
        {
            ActivatePhase2();
        }
        else if (currentPhase == 3)
        {
            ActivatePhase3();
        }
    }

    private void ActivatePhase2()
    {
        initialMoveSpeed *= moveSpeedMultiplier;
        fireRate /= moveSpeedMultiplier;
        // Additional behavior for Phase 2
        StartCoroutine(EnhancedBehavior());
    }

    private void ActivatePhase3()
    {
        initialMoveSpeed *= moveSpeedMultiplier;
        fireRate /= moveSpeedMultiplier;
        // Additional behavior for Phase 3
        StartCoroutine(UnpredictableBehavior());
    }

    private void ChooseNewTargetPosition()
    {
        targetPosition = transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, initialMoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            ChooseNewTargetPosition();
        }

        FlipSprite((targetPosition - transform.position).normalized);
    }

    private IEnumerator FireProjectiles()
    {
        while (true)
        {
            for (int i = 0; i < warningFlashes; i++)
            {
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(warningDuration);
                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(warningDuration);
            }

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

    private IEnumerator EnhancedBehavior()
    {
        while (currentPhase == 2)
        {
            ChooseNewTargetPosition();
            yield return new WaitForSeconds(wanderTime / moveSpeedMultiplier);

            StartCoroutine(AoEAttack());
            yield return new WaitForSeconds(phaseDuration / 2);
        }
    }

    private IEnumerator UnpredictableBehavior()
    {
        while (currentPhase == 3)
        {
            ChooseNewTargetPosition();
            yield return new WaitForSeconds(wanderTime / (moveSpeedMultiplier * 2));

            StartCoroutine(PowerfulAoEAttack());
            yield return new WaitForSeconds(phaseDuration / 4);
        }
    }

    private IEnumerator AoEAttack()
    {
        for (int i = 0; i < warningFlashes; i++)
        {
            spriteRenderer.color = Color.blue;
            yield return new WaitForSeconds(warningDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(warningDuration);
        }

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D player in hitPlayers)
        {
            if (player.CompareTag("Player"))
            {
                player.GetComponent<PlayerResources>().DamagePlayer(20);
            }
        }
    }
}
