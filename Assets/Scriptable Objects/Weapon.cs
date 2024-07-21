using System;
using System.Collections;
using System.Collections.Generic;
//Referencing the enums in the scripts directory, a breakdown of how to handle the flags is there
using Enums;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    [SerializeField] private WeaponTier weaponTier;
    [SerializeField] private WeaponClass weaponClass;
}
