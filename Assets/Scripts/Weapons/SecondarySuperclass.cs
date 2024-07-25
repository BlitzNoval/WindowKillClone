using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponBehaviour))]
public class SecondarySuperclass : MonoBehaviour
{
    protected WeaponBehaviour parentBehaviour;

    void Start()
    {
        parentBehaviour = GetComponent<WeaponBehaviour>();
        //Adding the secondary attack script to the secondaryeffect delegate in the weaponBehaviour script
        parentBehaviour.ThisSecondaryEffect += DoSecondaryEffect;
    }

    //This is overrideable by child classes
    protected virtual void DoSecondaryEffect()
    {
        throw new NotSupportedException("Please use a child class of this script");
    }
}
