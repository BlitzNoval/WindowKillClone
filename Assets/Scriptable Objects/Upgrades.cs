using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Upgrades : ScriptableObject
{
    [SerializeField] private Stats stats;
    [SerializeField] private WeaponTier tier;
    [SerializeField] private int amount;
}
