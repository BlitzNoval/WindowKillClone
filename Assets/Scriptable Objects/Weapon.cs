using System;
using System.Collections;
using System.Collections.Generic;
//Referencing the enums in the scripts directory, a breakdown of how to handle the flags is there
using Enums;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class Weapon : ScriptableObject
{
    [Serializable]
    public struct StatScaling
    {
        //Using the multiplier value. ie: 0.8 = 80% scaling
        [SerializeField] private float scalingAmount;
        [SerializeField] private Stats scalingType;
        public float ScalingAmount
        {
            get => scalingAmount;
            set => scalingAmount = value;
        }
        public Stats ScalingType
        {
            get => scalingType;
            set => scalingType = value;
        }
    }
    //Wrapper class to add multiple scaling types for serialization
    [Serializable]
    public struct StatScalingTier
    {
        [SerializeField] private StatScaling[] scalings;
        public StatScaling[] Scalings
        {
            get => scalings;
            set => scalings = value;
        }
    }
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private WeaponClass weaponClass;
    [SerializeField] private AttackType attackType;
    //[SerializeField] private ActivePassive secondaryEffectType;
    //[SerializeField] private bool hasSecondaryEffect;
    //[SerializeField] private WeaponTier weaponTier;
    [SerializeField] private float[] damagePerTier;
    [SerializeField] private StatScalingTier[] scalingPerTier;
    [SerializeField] private float[] attackSpeedPerTier;
    [SerializeField] private float[] critDamagePerTier;
    [SerializeField] private float[] critChancePerTier;
    [SerializeField] private float[] rangePerTier;
    [SerializeField] private float[] knockbackPerTier;
    [SerializeField] private float[] lifestealPerTier;
    [SerializeField] private int[] basePricePerTier;
    [SerializeField] private int[] piercePerTier;

    //public bool HasSecondaryEffect => hasSecondaryEffect;

    //public ActivePassive SecondaryEffectType => secondaryEffectType;

    public WeaponType WeaponType => weaponType;

    public WeaponClass WeaponClass => weaponClass;

    public AttackType AttackType => attackType;

    //public WeaponTier WeaponTier => weaponTier;

    public float[] DamagePerTier => damagePerTier;

    public StatScalingTier[] ScalingPerTier => scalingPerTier;

    public float[] AttackSpeedPerTier => attackSpeedPerTier;

    public float[] CritDamagePerTier => critDamagePerTier;

    public float[] CritChancePerTier => critChancePerTier;

    public float[] RangePerTier => rangePerTier;

    public float[] KnockbackPerTier => knockbackPerTier;

    public float[] LifestealPerTier => lifestealPerTier;

    public int[] BasePricePerTier => basePricePerTier;

    public int[] PiercePerTier => piercePerTier;
}
