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
                GetStat(PlayerBase.Instance.maxHP, "Max HP");
                break;
            case Stats.HPRegen:
                GetStat(PlayerBase.Instance.HPRegen, "HP Regeneration");
                break;
            case Stats.LifeSteal:
                GetStat(PlayerBase.Instance.lifeSteal, "% Life Steal");
                break;
            case Stats.Damage:
                GetStat(PlayerBase.Instance.damage, "% Damage");
                break;
            case Stats.MeleeDamage:                
                GetStat(PlayerBase.Instance.meleeDamage, "Melee Damage");
                break;
            case Stats.RangedDamage:                
                GetStat(PlayerBase.Instance.rangedDamage, "Ranged Damage");
                break;
            case Stats.ElementalDamage:                
                GetStat(PlayerBase.Instance.elementalDamage, "Elemental Damage");
                break;
            case Stats.AttackSpeed:                
                GetStat(PlayerBase.Instance.attackSpeed, "% Attack Speed");
                break;
            case Stats.CritChance:                
                GetStat(PlayerBase.Instance.critChance, "% Crit Chance");
                break;
            case Stats.Engineering:                
                GetStat(PlayerBase.Instance.engineering, "Engineering");
                break;
            case Stats.Range:                
                GetStat(PlayerBase.Instance.range, "Range");
                break;
            case Stats.Armor:                
                GetStat(PlayerBase.Instance.armor, "Armor");
                break;
            case Stats.Dodge:                
                GetStat(PlayerBase.Instance.dodge, "% Dodge");
                break;
            case Stats.Speed:                
                GetStat(PlayerBase.Instance.speed, "% Speed");
                break;
            case Stats.Luck:                
                GetStat(PlayerBase.Instance.luck, "Luck");
                break;
            case Stats.Harvesting:                
                GetStat(PlayerBase.Instance.harvesting, "Harvesting");
                break;
        }
    }

    private void GetStat(int value, string stat)
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
