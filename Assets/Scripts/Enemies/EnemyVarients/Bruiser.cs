using System.Collections;
using UnityEngine;

public class Bruiser : Enemy
{
    public float minChargeCooldown = 2.5f;
    public float maxChargeCooldown = 3.5f;
    public float chargeSpeedMultiplier = 2.0f;
    public float chargeRange = 5.0f; // Range within which the Bruiser starts charging
    public float warningDuration = 0.25f; // Duration of each warning flash
    public int warningFlashes = 2; // Number of warning flashes

    private float nextChargeTime;
    private bool isCharging;
    private Color originalColor;

    protected override void Start()
    {
        base.Start();
        SetNextChargeTime();

        // Store the original color
        originalColor = spriteRenderer.color;
    }

    protected override void Update()
    {
        base.Update();

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= chargeRange && Time.time >= nextChargeTime)
            {
                StartCoroutine(ChargeTowardsPlayer(player.position));
                SetNextChargeTime();
            }
            else if (!isCharging)
            {
                MoveTowardsPlayer();
            }
        }
    }

    private void SetNextChargeTime()
    {
        nextChargeTime = Time.time + Random.Range(minChargeCooldown, maxChargeCooldown);
    }

    private IEnumerator ChargeTowardsPlayer(Vector3 targetPosition)
    {
        isCharging = true;

        // Flash red twice as a warning
        for (int i = 0; i < warningFlashes; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(warningDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(warningDuration);
        }

        float originalSpeed = speed;
        speed *= chargeSpeedMultiplier;

        // Charge towards the locked position
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            FlipSprite(direction);
            yield return null;
        }

        // Stop for 0.5 seconds
        speed = 0;
        yield return new WaitForSeconds(0.2f);

        // Resume normal behavior
        speed = originalSpeed;
        isCharging = false;
    }
}
