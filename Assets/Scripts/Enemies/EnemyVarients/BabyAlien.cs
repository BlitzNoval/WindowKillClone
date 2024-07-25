using UnityEngine;

public class BabyAlien : Enemy
{
    protected override void Update()
    {
        base.Update();
        MoveTowardsPlayer();
    }
}
