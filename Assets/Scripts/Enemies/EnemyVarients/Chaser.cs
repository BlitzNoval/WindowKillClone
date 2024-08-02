using UnityEngine;

public class Chaser : Enemy
{
    public float minSpeed = 3.5f;
    public float maxSpeed = 4.5f;

    protected override void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        MoveTowardsPlayer();
    }
}
