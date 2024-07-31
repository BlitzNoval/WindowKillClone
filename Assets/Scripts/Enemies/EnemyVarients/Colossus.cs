using System.Collections;
using UnityEngine;

public class Colossus : Enemy
{
    [Header("Movement")]
    public float initialMoveSpeed = 5.0f;
    public float moveSpeedMultiplier = 1.1f;
    public float wanderRadius = 5f;

    [Header("Attack")]
    public float attackRange = 10.0f;
    public GameObject projectilePrefab;
    public float fireRate = 3.0f;
    public Transform[] firePoints;

    [Header("Visual Feedback")]
    public Color phaseChangeColor = Color.yellow;
    public Color buildUpColor = Color.red;
    public float warningDuration = 0.5f;

    [Header("Phases")]
    public float phaseDuration = 30.0f;
    public float growthFactor = 1.2f;

    [Header("Phase 1")]
    public int phase1ProjectileCount = 3;
    public float phase1SpreadAngle = 30f;

    [Header("Phase 2")]
    public int phase2ProjectileCount = 5;
    public float phase2SpreadAngle = 45f;

    [Header("Phase 3")]
    public int phase3ProjectileCount = 7;
    public float phase3SpreadAngle = 60f;
    public float chargeSpeed = 15f;
    public int chargeVolleyCount = 5;

    private Color originalColor;
    private Transform player;
    private float phaseTimer;
    private int currentPhase = 1;
    private bool isAttacking = false;
    private Vector3 wanderTarget;
    private float currentMoveSpeed;

    protected override void Start()
    {
        base.Start();
        originalColor = spriteRenderer.color;
        phaseTimer = phaseDuration;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentMoveSpeed = initialMoveSpeed;
        StartCoroutine(ManagePhases());
        SetWanderTarget();
    }

    protected override void Update()
    {
        base.Update();
        Move();
    }

    private void Move()
    {
        if (player != null)
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
        }
    }

    private void AdvancePhase()
    {
        currentPhase++;
        currentMoveSpeed *= moveSpeedMultiplier;
        transform.localScale *= growthFactor;
        StartCoroutine(PhaseChangeIndicator());

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

    private IEnumerator Phase2Behavior()
    {
        while (currentPhase == 2)
        {
            yield return StartCoroutine(BuildUpBeforeAttack());
            FireProjectilesInPattern(phase2ProjectileCount, phase2SpreadAngle);
            yield return new WaitForSeconds(fireRate * 0.75f);
        }
    }

    private IEnumerator Phase3Behavior()
    {
        while (currentPhase == 3)
        {
            yield return StartCoroutine(BuildUpBeforeAttack(true));
            FireProjectilesInPattern(phase3ProjectileCount, phase3SpreadAngle);
            yield return new WaitForSeconds(fireRate * 0.5f);
        }
    }

    private IEnumerator BuildUpBeforeAttack(bool chargeTowardsPlayer = false)
    {
        isAttacking = true;
        spriteRenderer.color = new Color(buildUpColor.r, buildUpColor.g, buildUpColor.b, 1f / 3f);
        yield return new WaitForSeconds(warningDuration);
        spriteRenderer.color = new Color(buildUpColor.r, buildUpColor.g, buildUpColor.b, 2f / 3f);
        yield return new WaitForSeconds(warningDuration);
        spriteRenderer.color = buildUpColor;

        if (chargeTowardsPlayer && player != null)
        {
            Vector3 chargeTarget = player.position;
            float originalMoveSpeed = currentMoveSpeed;
            currentMoveSpeed = chargeSpeed;

            while (Vector3.Distance(transform.position, chargeTarget) > 0.1f && isAttacking)
            {
                transform.position = Vector3.MoveTowards(transform.position, chargeTarget, currentMoveSpeed * Time.deltaTime);
                FlipSprite((chargeTarget - transform.position).normalized);
                FireProjectilesInPattern(chargeVolleyCount, 360f);
                yield return new WaitForSeconds(0.2f);
            }

            currentMoveSpeed = originalMoveSpeed;
        }

        yield return new WaitForSeconds(warningDuration);
        spriteRenderer.color = originalColor;
        isAttacking = false;
    }

    private void FireProjectilesInPattern(int projectileCount, float spreadAngle)
    {
        if (projectilePrefab != null && firePoints.Length > 0)
        {
            foreach (Transform firePoint in firePoints)
            {
                Vector3 direction = (player.position - firePoint.position).normalized;
                float angleStep = spreadAngle / (projectileCount - 1);
                float startAngle = -spreadAngle / 2;

                for (int i = 0; i < projectileCount; i++)
                {
                    float currentAngle = startAngle + angleStep * i;
                    Vector3 projectileDirection = Quaternion.Euler(0, 0, currentAngle) * direction;
                    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                    projectile.GetComponent<EnemyProjectile>().Initialize(projectileDirection);
                }
            }
        }
    }
}