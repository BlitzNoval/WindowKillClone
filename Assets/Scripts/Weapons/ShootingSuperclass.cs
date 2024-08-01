using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

[RequireComponent(typeof(WeaponBehaviour))]
public class ShootingSuperclass : MonoBehaviour
{
    protected WeaponBehaviour parentBehaviour;

    void Start()
    {
        parentBehaviour = GetComponent<WeaponBehaviour>();
        //Adding the shooting script to the delegate in the weaponBehaviour script
        parentBehaviour.ThisShootingEffect += DoShootingEffect;
    }

    //This is overrideable by child classes
    //We expect to have a thrust script, a sweep script, and a shooting script
    protected virtual void DoShootingEffect(Vector2 direction)
    {
        throw new NotSupportedException("Please use a child class of this script");
    }

    public virtual void OnHitboxHit(Collider2D other)
    {
        throw new NotSupportedException("Please use a child class of this script");
    }
}