using UnityEngine;

public class BabyAlien : Enemy
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        MoveTowardsPlayer();
    }
}
