using System;
using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    //singleton for stat logic
    public static PlayerBase Instance { get; private set; }

    #region Icons
    public Sprite[] icons;
    #endregion

    [Space(10)]

    #region Displayed Stats
    [Header("Displayed Stats")]

    public PrimaryStats primaryStats = new PrimaryStats();
    public PrimaryStats calcPrimaryStats = new PrimaryStats();

    public SecondaryStats secondaryStats = new SecondaryStats();
    #endregion


    private void Awake()
    {
        //ensure that there is only one of this gameobject
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        primaryStats.maxHP = 10;
    }


    /// <summary>
    /// use this function to update the players stats
    /// </summary>
    /// <param name="stat">     stat the item wants to change </param>
    /// <param name="amount">   amount the stat changes as an int </param>
    public void UpdateStat(Stats stat, int amount)
    {
        switch (stat)
        {
            case Stats.MaxHP:
                primaryStats.maxHP += amount;
                break;
            case Stats.HPRegen:
                primaryStats.HPRegen += amount;
                break;
            case Stats.LifeSteal:
                primaryStats.lifeSteal += amount;
                break;
            case Stats.Damage:
                primaryStats.damage += amount;
                break;
            case Stats.MeleeDamage:
                primaryStats.meleeDamage += amount;
                break;
            case Stats.RangedDamage:
                primaryStats.rangedDamage += amount;
                break;
            case Stats.ElementalDamage:
                primaryStats.elementalDamage += amount;
                break;
            case Stats.AttackSpeed:
                primaryStats.attackSpeed += amount;
                break;
            case Stats.CritChance:
                primaryStats.critChance += amount;
                break;
            case Stats.Engineering:
                primaryStats.engineering += amount;
                break;
            case Stats.Range:
                primaryStats.range += amount;
                // --------------------------------------------LOOK HERE YOAV--------------------------------------------------
                // Add updateRange() interface ****** HERE ********
                break;
            case Stats.Armor:
                primaryStats.armor += amount;
                break;
            case Stats.Dodge:
                primaryStats.dodge += amount;
                break;
            case Stats.Speed:
                primaryStats.speed += amount;
                break;
            case Stats.Luck:
                primaryStats.luck += amount;
                break;
            case Stats.Harvesting:
                primaryStats.harvesting += amount;
                break;
        }
    }
    
    //Function overload for secondary stats
    //todo: implement overload for calculation as well
    public void UpdateStat(SecondaryStats stat, int amount)
    {
        
    }

    /// <summary>
    /// this function calculates the stat after the upgrade points
    /// </summary>
    /// <param name="stat"> the stat that you want to calculate</param>
    public void CalculateStat(Stats stat)
    {
        switch (stat)
        {
            case Stats.MaxHP:
                calcPrimaryStats.maxHP = primaryStats.maxHP;
                PlayerResources.Instance.maxHealth = (int)primaryStats.maxHP;
                PlayerResources.Instance.health = (int)primaryStats.maxHP;
                break;
            case Stats.HPRegen:
                //formulas given on wiki
                float HPEveryXSeconds = 5 / (1 + ((primaryStats.HPRegen - 1) / 2.25f)); 
                calcPrimaryStats.HPRegen = HPEveryXSeconds;
                break;
            case Stats.LifeSteal:
                calcPrimaryStats.lifeSteal = primaryStats.lifeSteal / 100;
                break;
            case Stats.Damage:
                calcPrimaryStats.damage = primaryStats.damage / 100;
                break;
            case Stats.MeleeDamage:
                calcPrimaryStats.meleeDamage = primaryStats.meleeDamage;
                break;
            case Stats.RangedDamage:
                calcPrimaryStats.rangedDamage = primaryStats.rangedDamage;
                break;
            case Stats.ElementalDamage:
                calcPrimaryStats.elementalDamage = primaryStats.elementalDamage;
                break;
            case Stats.AttackSpeed:
                calcPrimaryStats.attackSpeed = primaryStats.attackSpeed / 100;
                break;
            case Stats.CritChance:
                calcPrimaryStats.critChance = primaryStats.critChance / 100;
                break;
            case Stats.Engineering:
                calcPrimaryStats.engineering = primaryStats.engineering;
                break;
            case Stats.Range:
                calcPrimaryStats.range = primaryStats.range;
                break;
            case Stats.Armor:
                calcPrimaryStats.armor = primaryStats.armor * 0.0667f;
               break;
            case Stats.Dodge:
                calcPrimaryStats.dodge = primaryStats.dodge / 100;
                break;
            case Stats.Speed:
                calcPrimaryStats.speed = primaryStats.speed / 100;
                break;
            case Stats.Luck:
                calcPrimaryStats.luck = primaryStats.luck / 100;
                break;
            case Stats.Harvesting:
                calcPrimaryStats.harvesting = primaryStats.harvesting;
                break;
        }

        Debug.Log(calcPrimaryStats.HPRegen);

    }
}

[Serializable]
public struct PrimaryStats
{
    public float maxHP;
    public float HPRegen;       //restores 1HP in n seconds
    public float lifeSteal;     //attacks have % chance to heal 1HP (each weapon individually)
    public float damage;          //increases all damage by 1% per damage point
    public float meleeDamage;     //modifies base attack of melee weapons
    public float rangedDamage;    //modifies base attack of ranged weapons
    public float elementalDamage; //modifies base attack of elemental weapons
    public float attackSpeed;   //attack x% faster [not to engineering turrents or mines]
    public float critChance;    //x% increased chance to deal crit hit
    public float engineering;     //increase the power of structures (not clear how)
    public float range;           //max range increased by x (also increases cooldown of melee)
    public float armor;         //reduce incoming damage by x%
    public float dodge;         //have x% chance to dodge attacks
    public float speed;         //move x% faster
    public float luck;          //have x% more chance to find items/consumables on kill and increase upgrade/shop rarity
    public float harvesting;    //You earn x materials and XP at the end of a wave. Increased by 5% every time it activates. (Rounded up)
}

[Serializable]
public struct SecondaryStats
{
    public float XPGain;
    public float explosionDamage;
    public float explosionSize;
    public float bounces;
    public float piercing;
    public float piercingDamage;
    public float buringDamage;
    public float burningSpeed;
    public float buringSpread;
    public float knockback;

    public float standStill_armor;
    public float standStill_dodge;
    public float standStill_damage;

    public float emenies_more;
    public float emenies_less;
    public float enemies_speed;
    public float enemies_slow;

    public float consumableHeal;
    public float materialsHealing;
    public float HPPerMaterial;

    public float pickupRange;
    public float trees;
    public float materialsInCrates;
    public float chanceForDoubleMaterials;
    public float chanceForInstantMaterialPickup;
    public float chanceToDamangeOnMaterialPickup;

    public float itemsPrice;
    public float freeRerolls;
    public float itemRecyclingRewards;
    public float materialInterestGain;
}
