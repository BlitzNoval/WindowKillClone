using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitboxSendBehaviour : MonoBehaviour
{
    [SerializeField] private ShootingSuperclass parentShooter;

    private void Start()
    {
        parentShooter = transform.parent.GetComponent<ShootingSuperclass>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        parentShooter.OnHitboxHit(other);
    }
}
