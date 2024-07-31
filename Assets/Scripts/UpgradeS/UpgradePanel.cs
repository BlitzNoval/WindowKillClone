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
        upgradeTier = tier;
        GetComponent<Image>().color = tierColors[tier];
        icon.sprite = upgrade.icon;
        //make icon the same
    }
    public void ChooseUpgrade()
    {
        PlayerBase.Instance.UpdateStat(upgrade.stats, upgrade.amount[upgradeTier]);
        PlayerBase.Instance.CalculateStat(upgrade.stats);
        StatDisplay.Instance.UpgradteStatColumns();

        PlayerResources.Instance.levelUp--;

        if (PlayerResources.Instance.levelUp > 0)
        {
            UpgradeManager.Instance.OpenUpgradePanel();
        }
        else
        {
            UpgradeManager.Instance.upgradeUI.SetActive(false);
            GameManager.Instance.SwitchState(GameManager.Instance.shopState);
        }

        // go to next upgrade panel or to item store;
    }
}
