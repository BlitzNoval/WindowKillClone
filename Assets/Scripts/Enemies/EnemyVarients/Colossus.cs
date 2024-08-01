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

    private void SetWanderTarget()
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

        switch (currentPhase)
        {
            case 2:
                StartCoroutine(Phase2Behavior());
                break;
            case 3:
                StartCoroutine(Phase3Behavior());
                break;
        }
    }

    private IEnumerator PhaseChangeIndicator()
    {
        spriteRenderer.color = phaseChangeColor;
        yield return new WaitForSeconds(1.0f);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator Phase1Behavior()
    {
        while (currentPhase == 1)
        {
            yield return StartCoroutine(BuildUpBeforeAttack());
            FireProjectilesInPattern(phase1ProjectileCount, phase1SpreadAngle);
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

    private IEnumerator PowerfulAoEAttack()
    {
        for (int i = 0; i < warningFlashes; i++)
        {
            spriteRenderer.color = Color.magenta;
            yield return new WaitForSeconds(warningDuration / 2);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(warningDuration / 2);
        }

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange * 1.5f);
        foreach (Collider2D player in hitPlayers)
        {
            if (player.CompareTag("Player"))
            {
                player.GetComponent<PlayerResources>().DamagePlayer(40);
            }
        }
    }
}
