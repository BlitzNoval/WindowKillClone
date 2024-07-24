using Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatCol : MonoBehaviour
{
    public Stats stat;
    public Image icon;
    public TextMeshProUGUI statName;
    public TextMeshProUGUI statValue;

    public Color[] textColour;
    
    /// <summary>
    /// Set the stat after each purchase or upgrade
    /// </summary>
    public void SetStat()
    {
        switch (stat)
        {
            case Stats.MaxHP:
                GetStat(PlayerBase.Instance.primaryStats.maxHP, "Max HP");
                break;
            case Stats.HPRegen:
                GetStat(PlayerBase.Instance.primaryStats.HPRegen, "HP Regeneration");
                break;
            case Stats.LifeSteal:
                GetStat(PlayerBase.Instance.primaryStats.lifeSteal, "% Life Steal");
                break;
            case Stats.Damage:
                GetStat(PlayerBase.Instance.primaryStats.damage, "% Damage");
                break;
            case Stats.MeleeDamage:                
                GetStat(PlayerBase.Instance.primaryStats.meleeDamage, "Melee Damage");
                break;
            case Stats.RangedDamage:                
                GetStat(PlayerBase.Instance.primaryStats.rangedDamage, "Ranged Damage");
                break;
            case Stats.ElementalDamage:                
                GetStat(PlayerBase.Instance.primaryStats.elementalDamage, "Elemental Damage");
                break;
            case Stats.AttackSpeed:                
                GetStat(PlayerBase.Instance.primaryStats.attackSpeed, "% Attack Speed");
                break;
            case Stats.CritChance:                
                GetStat(PlayerBase.Instance.primaryStats.critChance, "% Crit Chance");
                break;
            case Stats.Engineering:                
                GetStat(PlayerBase.Instance.primaryStats.engineering, "Engineering");
                break;
            case Stats.Range:                
                GetStat(PlayerBase.Instance.primaryStats.range, "Range");
                break;
            case Stats.Armor:                
                GetStat(PlayerBase.Instance.primaryStats.armor, "Armor");
                break;
            case Stats.Dodge:                
                GetStat(PlayerBase.Instance.primaryStats.dodge, "% Dodge");
                break;
            case Stats.Speed:                
                GetStat(PlayerBase.Instance.primaryStats.speed, "% Speed");
                break;
            case Stats.Luck:                
                GetStat(PlayerBase.Instance.primaryStats.luck, "Luck");
                break;
            case Stats.Harvesting:                
                GetStat(PlayerBase.Instance.primaryStats.harvesting, "Harvesting");
                break;
        }
    }

    private void GetStat(float value, string stat)
    {
        statValue.text = value.ToString();
        if (value > 0)
        {
            statValue.color = textColour[0];
        }
        else if (value < 0)
        {
            statValue.color = textColour[1];
        }
        else
        {
            statValue.color = textColour[2];
        }
        statName.text = stat;
        //add the icon here
    }
}
