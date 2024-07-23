using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public GameObject[] upgradePanels;
    public UpgradeScale[] upgradeScales;
    public Upgrades[] upgrades;
    public float[] chanceThresholds = new float[4];
    public GameObject upgradeUI;

    private void Awake()
    {
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
        CalcChanceThresholds();
        ChooseUpgrades();
    }


    /// <summary>
    /// Applys the upgrade to the player
    /// </summary>
    /// <param name="upgrade"> the selected upgrade</param>

    public void ChooseUpgrades()
    {
        Upgrades[] selectedUpgrades = RandomUpgrades();

        for (int i = 0; i < upgradePanels.Length; i++)
        {
            Upgrades newUpgrade = selectedUpgrades[i];

            upgradePanels[i].GetComponent<UpgradePanel>().upgrade = newUpgrade;

            float randNum = Random.Range(0, 1f);

            if (randNum > chanceThresholds[1])
            {
                upgradePanels[i].GetComponent<UpgradePanel>().SetUpgradePanel(0);
            }
            else if (randNum > chanceThresholds[2])
            {
                upgradePanels[i].GetComponent<UpgradePanel>().SetUpgradePanel(1);
            }
            else if (randNum > chanceThresholds[3])
            {
                upgradePanels[i].GetComponent<UpgradePanel>().SetUpgradePanel(2);
            }
            else
            {
                upgradePanels[i].GetComponent<UpgradePanel>().SetUpgradePanel(3);
            }

        }
    }

    private Upgrades[] RandomUpgrades()
    {
        List<Upgrades> l_upgrades = upgrades.ToList();
        Upgrades[] selectedUpgrades = new Upgrades[4];

        for (int i = 0; i < 4; i++)
        {
            selectedUpgrades[i] = l_upgrades[Random.Range(0, l_upgrades.Count)];
            l_upgrades.Remove(l_upgrades[i]);
        }

        return selectedUpgrades;
    }

    /// <summary>
    /// calculates the new chance thresholds for each rarity when teh player levels up
    /// </summary>
    private void CalcChanceThresholds()
    {
        for (int i = 0; i < chanceThresholds.Length; i++)
        {
            int minLevel = upgradeScales[i].minLevel;
            float baseChance = upgradeScales[i].baseChance;
            float chancePerLevel = upgradeScales[i].chancePerLevel;
            float maxChance = upgradeScales[i].maxChance;

            float newThreshold = ((chancePerLevel * (PlayerBase.Instance.level - minLevel)) + baseChance) * (1 + PlayerBase.Instance.luck / 100);
            newThreshold = Mathf.Clamp(newThreshold, 0, maxChance);

            chanceThresholds[i] = newThreshold;
        }
    }
}
