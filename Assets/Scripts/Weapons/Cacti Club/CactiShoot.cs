using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactiShoot : ShootingSuperclass
{
    protected override void DoShootingEffect(Vector2 direction)
    {
        Debug.Log("pew pew cactus");
    }
}
