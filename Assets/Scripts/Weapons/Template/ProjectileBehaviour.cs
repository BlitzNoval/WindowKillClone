using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float damage;
    //The distance the projectile will travel until it times out
    public float maxRange;
    //The distance the projectile has travelled
    private float currentRange;
    private float pierce;
    public WeaponBehaviour parentWeapon;
}