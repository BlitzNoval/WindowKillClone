using Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public Upgrades upgrade;

    public TextMeshProUGUI itemname;
    public Image icon;
    public TextMeshProUGUI statDescription;
    public int upgradeTier;
    public Color[] tierColors = new Color[4];

    /// <summary>
    /// call this funciton when upgrade has been set
    /// </summary>
    public void SetUpgradePanel(int tier)
    {
        itemname.text = $"{upgrade.name} {tier + 1}";
        statDescription.text = $"+{upgrade.amount[tier]} {upgrade.stats}";
        upgradeTier = tier ;
        GetComponent<Image>().color = tierColors[tier];
        //make icon the same
    }
    public void ChooseUpgrade()
    {
        PlayerBase.Instance.UpdateStat(upgrade.stats, upgrade.amount[upgradeTier]);
        PlayerBase.Instance.CalculateStat(upgrade.stats);
        UpgradeManager.Instance.upgradeUI.SetActive(false);
        // go to next upgrade panel or to item store;
    }
}
