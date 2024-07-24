using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float damage;
    //The distance the projectile will travel until it times out
    public float maxRange;
    public int maxPierce;

    private int currentPierce;
    //The distance the projectile has travelled
    private float currentRange;
    public WeaponBehaviour parentWeapon;
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //The bullet has hit an enemy
        }
    }
}