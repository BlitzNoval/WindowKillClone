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

    /// <summary>
    /// call this funciton when upgrade has been set
    /// </summary>
    public void SetUpgradePanel()
    {
        itemname.text = upgrade.name;
        statDescription.text = $"+{upgrade.amount} {upgrade.stats}";
        //make icon the same
    }
    public void ChooseUpgrade()
    {
        UpgradeManager.Instance.ApplyUpgrade(upgrade);
        // go to next upgrade panel or to item store;
    }
}
