using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponBehaviour))]
public class ShootingSuperclass : MonoBehaviour
{
    private WeaponBehaviour parentBehaviour;

    void Start()
    {
        parentBehaviour = GetComponent<WeaponBehaviour>();
        //Adding the shooting script to the delegate in the weaponBehaviour script
        parentBehaviour.ThisShootingEffect += DoShootingEffect;
    }

    //This is overrideable by child classes
    protected virtual void DoShootingEffect()
    {
        throw new NotSupportedException("Please use a child class of this script");
    }
}