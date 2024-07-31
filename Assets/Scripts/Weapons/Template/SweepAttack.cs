using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepAttack : ShootingSuperclass
{
    protected override void DoShootingEffect(Vector2 direction)
    {
        //base.DoShootingEffect(direction);
        StartCoroutine(DoSweep());
    }

    public override void OnHitboxHit(Collider2D other)
    {
        Debug.Log("WHOAAA");
        other.gameObject.GetComponent<Enemy>().TakeDamage(parentBehaviour.CalculateDamage());
    }

    private IEnumerator DoSweep()
    {
        float animationTime = parentBehaviour.CalculateCooldown();
        yield break;
    }
}
