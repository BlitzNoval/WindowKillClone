using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Upgrades : ScriptableObject
{
    public Stats stats;
    public WeaponTier tier;
    public int amount;
}
