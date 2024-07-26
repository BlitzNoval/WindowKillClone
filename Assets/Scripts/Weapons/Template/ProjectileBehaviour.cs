using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ProjectileBehaviour : MonoBehaviour
{
    private Vector2 startingPostion;
    public float damage;
    //The distance the projectile will travel until it times out
    public float maxRange;
    public int maxPierce;

    private int currentPierce;
    //The distance the projectile has travelled
    [SerializeField] private float currentRange;
    public WeaponBehaviour parentWeapon;

    private void FixedUpdate()
    {
        currentRange = Vector2.Distance(startingPostion, transform.position);
        //Range checks
        if (Vector2.Distance(startingPostion, transform.position) >= maxRange)
        {
            Destroy(this.gameObject);
        }
    }

    public void DoSetup()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().mass = 1;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CircleCollider2D>().isTrigger = true;
        currentPierce = maxPierce;
        currentRange = maxRange;
        startingPostion = transform.position;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //The bullet has hit an enemy
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            currentPierce--;
            if (currentPierce < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}