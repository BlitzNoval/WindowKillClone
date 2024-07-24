using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    //singleton for stat logic
    public static PlayerBase Instance { get; private set; }

    public int level;

    #region Icons
    public Sprite[] icons;
    #endregion

    [Space(10)]

    #region Displayed Stats
    [Header("Displayed Stats")]
    public int maxHP;
    public int HPRegen;       //restores 1HP in n seconds
    public int lifeSteal;     //attacks have % chance to heal 1HP (each weapon individually)
    public int damage;          //increases all damage by 1% per damage point
    public int meleeDamage;     //modifies base attack of melee weapons
    public int rangedDamage;    //modifies base attack of ranged weapons
    public int elementalDamage; //modifies base attack of elemental weapons
    public int attackSpeed;   //attack x% faster [not to engineering turrents or mines]
    public int critChance;    //x% increased chance to deal crit hit
    public int engineering;     //increase the power of structures (not clear how)
    public int range;           //max range increased by x (also increases cooldown of melee)
    public int armor;         //reduce incoming damage by x%
    public int dodge;         //have x% chance to dodge attacks
    public int speed;         //move x% faster
    public int luck;          //have x% more chance to find items/consumables on kill and increase upgrade/shop rarity
    public int harvesting;    //You earn x materials and XP at the end of a wave. Increased by 5% every time it activates. (Rounded up
    #endregion

    #region Calculated Stats
    [Header("Calculated Stats")]
    public int c_maxHP;
    public float c_HPRegen;       
    public float c_lifeSteal;     
    public float c_damage;          
    public int c_meleeDamage;     
    public int c_rangedDamage;    
    public int c_elementalDamage; 
    public float c_attackSpeed;
    public float c_critChance;    
    public int c_engineering;     
    public int c_range;           
    public float c_armor;         
    public float c_dodge;         
    public float c_speed;         
    public float c_luck;          
    public int c_harvesting;
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
                maxHP += amount;
                break;
            case Stats.HPRegen:
                HPRegen += amount;
                break;
            case Stats.LifeSteal:
                lifeSteal += amount;
                break;
            case Stats.Damage:
                damage += amount;
                break;
            case Stats.MeleeDamage:
                meleeDamage += amount;
                break;
            case Stats.RangedDamage:
                rangedDamage += amount;
                break;
            case Stats.ElementalDamage:
                elementalDamage += amount;
                break;
            case Stats.AttackSpeed:
                attackSpeed += amount;
                break;
            case Stats.CritChance:
                critChance += amount;
                break;
            case Stats.Engineering:
                engineering += amount;
                break;
            case Stats.Range:
                range += amount;
                break;
            case Stats.Armor:
                armor += amount;
                break;
            case Stats.Dodge:
                dodge += amount;
                break;
            case Stats.Speed:
                speed += amount;
                break;
            case Stats.Luck:
                luck += amount;
                break;
            case Stats.Harvesting:
                harvesting += amount;
                break;
        }
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
                c_maxHP = maxHP; 
                break;
            case Stats.HPRegen:
                //formulas given on wiki
                float HPEveryXSeconds = 5 / (1 + ((HPRegen - 1) / 2.25f)); 
                c_HPRegen = 1 / HPEveryXSeconds;
                break;
            case Stats.LifeSteal:
                c_lifeSteal = lifeSteal / 100;
                break;
            case Stats.Damage:
                c_damage = damage / 100;
                break;
            case Stats.MeleeDamage:
                c_meleeDamage = meleeDamage;
                break;
            case Stats.RangedDamage:
                c_rangedDamage = rangedDamage;
                break;
            case Stats.ElementalDamage:
                c_elementalDamage = elementalDamage;
                break;
            case Stats.AttackSpeed:
                c_attackSpeed = attackSpeed / 100;
                break;
            case Stats.CritChance:
                c_critChance = critChance / 100;
                break;
            case Stats.Engineering:
                c_engineering = engineering;
                break;
            case Stats.Range:
                c_range = range;
                break;
            case Stats.Armor:
                c_armor = armor * 0.0667f;
               break;
            case Stats.Dodge:
                c_dodge = dodge / 100;
                break;
            case Stats.Speed:
                c_speed = speed / 100;
                break;
            case Stats.Luck:
                c_luck = luck / 100;
                break;
            case Stats.Harvesting:
                c_harvesting = harvesting;
                break;
        }
    }
}
