using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    //singleton
    public static PlayerBase Instance { get; private set; }

    #region Displayed Stats
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
    public void UpdateStat(PlayerStats stat, int amount)
    {
        switch (stat)
        {
            case PlayerStats.MaxHP:
                maxHP += amount;
                break;
            case PlayerStats.HPRegen:
                HPRegen += amount;
                break;
            case PlayerStats.LifeSteal:
                lifeSteal += amount;
                break;
            case PlayerStats.Damage:
                damage += amount;
                break;
            case PlayerStats.MeleeDamage:
                meleeDamage += amount;
                break;
            case PlayerStats.RangedDamage:
                rangedDamage += amount;
                break;
            case PlayerStats.ElementalDamage:
                elementalDamage += amount;
                break;
            case PlayerStats.AttackSpeed:
                attackSpeed += amount;
                break;
            case PlayerStats.CritChance:
                critChance += amount;
                break;
            case PlayerStats.Engineering:
                engineering += amount;
                break;
            case PlayerStats.Range:
                range += amount;
                break;
            case PlayerStats.Armor:
                armor += amount;
                break;
            case PlayerStats.Dodge:
                dodge += amount;
                break;
            case PlayerStats.Speed:
                speed += amount;
                break;
            case PlayerStats.Luck:
                luck += amount;
                break;
            case PlayerStats.Harvesting:
                harvesting += amount;
                break;
        }
    }
}
