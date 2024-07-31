using System.Collections;
using UnityEngine;

public class ThrustAttack : ShootingSuperclass
{
    public float thrustRange = 1.0f;
    public float thrustCooldown = 1.0f;
    private bool isAttacking = false;
    private Vector3 originalPosition;
    
    void Start()
    {
        originalPosition = transform.position;
    }

    protected override void DoShootingEffect(Vector2 direction)
    {
        if (!isAttacking)
        {
            StartCoroutine(PerformThrustAttack(direction));
        }
    }

    private IEnumerator PerformThrustAttack(Vector2 direction)
    {
        isAttacking = true;
        // Enable the hitbox
        GetComponent<Collider2D>().enabled = true;

        // Move the hitbox forward
        Vector3 targetPosition = originalPosition + (Vector3)direction * thrustRange;
        float elapsedTime = 0f;
        while (elapsedTime < thrustCooldown / 2)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / (thrustCooldown / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move the hitbox back to the original position
        elapsedTime = 0f;
        while (elapsedTime < thrustCooldown / 2)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, (elapsedTime / (thrustCooldown / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Disable the hitbox
        GetComponent<Collider2D>().enabled = false;
        isAttacking = false;
    }

    public override void OnHitboxHit(Collider2D other)
    {
        Debug.Log("WHOAAA");
        other.gameObject.GetComponent<Enemy>().TakeDamage(parentBehaviour.CalculateDamage());
    }
}
