using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private PlayerBase playerStats;
    [SerializeField] private Weapon weaponData;

    [Header("Watchers")] 
    [SerializeField] private WeaponTier currentTier;

    [SerializeField] private bool canAttack;

    public WeaponTier CurrentTier
    {
        get => currentTier;
        set => currentTier = value;
    }

    public delegate void SecondaryEffect();

    public delegate void ShootingEffect();

    [SerializeField] private ShootingEffect thisShootingEffect;
    [SerializeField] private SecondaryEffect thisSecondaryEffect;

    public ShootingEffect ThisShootingEffect
    {
        get => thisShootingEffect;
        set => thisShootingEffect = value;
    }
    
    public SecondaryEffect ThisSecondaryEffect
    {
        get => thisSecondaryEffect;
        set => thisSecondaryEffect = value;
    }
    
    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
        canAttack = true;
    }
    
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            thisSecondaryEffect?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            thisShootingEffect?.Invoke();
        }
        */
    }

    /// <summary>
    /// Method used to get the damage of a weapon as it attacks
    /// </summary>
    private float CalculateDamage()
    {
        float result = 0;
        //pulling damage percentile modifier from the player stats
        float damageStat = playerStats.c_damage;
        
        //pulling weapon damage from the attached scriptableObject
        float weaponDamage = weaponData.DamagePerTier[(int)currentTier];
        
        //scaling calculations
        float scaleValue = 0;
        
        //iterating through each scaling value for the current weapon tier
        foreach (var scaleInstance in weaponData.ScalingPerTier[(int)currentTier].Scalings)
        {
            switch (scaleInstance.ScalingType)
            {
                case Stats.MaxHP:
                    scaleValue += playerStats.c_maxHP * scaleInstance.ScalingAmount;
                    break;
                case Stats.HPRegen:
                    scaleValue += playerStats.c_HPRegen * scaleInstance.ScalingAmount;
                    break;
                case Stats.LifeSteal:
                    scaleValue += playerStats.c_lifeSteal * scaleInstance.ScalingAmount;
                    break;
                case Stats.Damage:
                    scaleValue += playerStats.c_damage * scaleInstance.ScalingAmount;
                    break;
                case Stats.MeleeDamage:
                    scaleValue += playerStats.c_meleeDamage * scaleInstance.ScalingAmount;
                    break;
                case Stats.RangedDamage:
                    scaleValue += playerStats.c_rangedDamage * scaleInstance.ScalingAmount;
                    break;
                case Stats.ElementalDamage:
                    scaleValue += playerStats.c_elementalDamage * scaleInstance.ScalingAmount;
                    break;
                case Stats.AttackSpeed:
                    scaleValue += playerStats.c_attackSpeed * scaleInstance.ScalingAmount;
                    break;
                case Stats.CritChance:
                    scaleValue += playerStats.c_critChance * scaleInstance.ScalingAmount;
                    break;
                case Stats.Engineering:
                    scaleValue += playerStats.c_engineering * scaleInstance.ScalingAmount;
                    break;
                case Stats.Range:
                    scaleValue += playerStats.c_range * scaleInstance.ScalingAmount;
                    break;
                case Stats.Armor:
                    scaleValue += playerStats.c_armor * scaleInstance.ScalingAmount;
                    break;
                case Stats.Dodge:
                    scaleValue += playerStats.c_dodge * scaleInstance.ScalingAmount;
                    break;
                case Stats.Speed:
                    scaleValue += playerStats.c_speed * scaleInstance.ScalingAmount;
                    break;
                case Stats.Luck:
                    scaleValue += playerStats.c_luck * scaleInstance.ScalingAmount;
                    break;
                case Stats.Harvesting:
                    scaleValue += playerStats.c_harvesting * scaleInstance.ScalingAmount;
                    break;
            }
        }

        weaponDamage += scaleValue;
        
        //multiplying weapon damage by the percentage of the damage stat
        result = weaponDamage * (1 + damageStat/100);
        return result;
    }

    /// <summary>
    /// Cooldown timer method
    /// </summary>
    private IEnumerator DoWeaponCooldown()
    {
        float waitTime = 0;
        //This is a value in seconds
        float weaponSpeed = weaponData.AttackSpeedPerTier[(int)currentTier];
        //percentage modifier of base speed
        float attackSpeedStat = playerStats.c_attackSpeed;
        // There are some exceptions with changes to certain weapons etc, but because I couldn't find a sheet of which
        // weapons get what effects, they will be ignored for now
        
        //cooldown = baseTime/modifiedSpeed
        float modifiedSpeed = (100 + attackSpeedStat)/100;
        waitTime = weaponSpeed / modifiedSpeed;
        yield return new WaitForSeconds(waitTime);
        canAttack = true;
    }
}
