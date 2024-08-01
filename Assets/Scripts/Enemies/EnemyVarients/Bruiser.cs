using System.Collections;
using UnityEngine;

public class Bruiser : Enemy
{
    public float minChargeCooldown = 2.5f;
    public float maxChargeCooldown = 3.5f;
    public float chargeSpeedMultiplier = 2.0f;
    public float chargeDistance = 10.0f; // Increased charge distance
    public float warningDuration = 0.25f; // Duration of each warning flash
    public int warningFlashes = 2; // Number of warning flashes
    public int damage = 10; // Damage dealt to the player on touch

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

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player != null)
        {
            if (Time.time >= nextChargeTime && !isCharging)
            {
                StartCoroutine(ChargeTowardsPlayer());
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

    private IEnumerator ChargeTowardsPlayer()
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

        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 chargeTarget = transform.position + direction * chargeDistance;

        // Charge towards the target position
        while (Vector3.Distance(transform.position, chargeTarget) > 0.1f)
        {
            transform.position += direction * speed * Time.deltaTime;
            FlipSprite(direction);
            yield return null;
        }

        // Stop for 0.5 seconds
        speed = 0;
        yield return new WaitForSeconds(0.5f);

        // Resume normal behavior
        speed = originalSpeed;
        isCharging = false;
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        FlipSprite(direction);
    }

    private void FlipSprite(Vector3 direction)
    {
        // Flip the sprite based on the direction
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
