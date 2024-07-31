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

    public int currentRerollPrice;
    public int rerollIncrease;

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
        //OpenUpgradePanel();
        //CalculateRerollIncrease();
    }

    /// <summary>
    /// chooses 4 upgrades when the player levels up
    /// </summary>
    private void ChooseUpgrades()
    {
        Upgrades[] selectedUpgrades = RandomUpgrades();

        if (PlayerResources.Instance.level % 5 != 0)
        {
            for (int i = 0; i < upgradePanels.Length; i++)
            {
                Upgrades newUpgrade = selectedUpgrades[i];

                upgradePanels[i].GetComponent<UpgradePanel>().upgrade = newUpgrade;

                //generate a random number and check if it is above the rarity threshold
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
        else
        {
            if (PlayerResources.Instance.level / 5 == 1)
            {
                SetUpgradesToSameRarity(selectedUpgrades, 1);
            }
            else if (PlayerResources.Instance.level / 5 <= 4)
            {
                SetUpgradesToSameRarity(selectedUpgrades, 2);
            }
            else if (PlayerResources.Instance.level / 5 >= 5)
            {
                SetUpgradesToSameRarity(selectedUpgrades, 3);
            }
        }
    }

    /// <summary>
    /// sets all the upgrades to the smae tier to match the games rarity guarentee table
    /// </summary>
    /// <param name="selectedUpgrades"> the array of selects upgrades </param>
    /// <param name="tier"> the tier you want the upgrades to be -1 </param>
    private void SetUpgradesToSameRarity(Upgrades[] selectedUpgrades, int tier)
    {
        for (int i = 0; i < upgradePanels.Length; i++)
        {
            Upgrades newUpgrade = selectedUpgrades[i];
            upgradePanels[i].GetComponent<UpgradePanel>().upgrade = newUpgrade;
            upgradePanels[i].GetComponent<UpgradePanel>().SetUpgradePanel(tier);
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

            float newThreshold = ((chancePerLevel * (PlayerResources.Instance.level - minLevel)) + baseChance) * (1 + PlayerBase.Instance.primaryStats.luck / 100);
            newThreshold = Mathf.Clamp(newThreshold, 0, maxChance);

            chanceThresholds[i] = newThreshold;
        }
    }

    /// <summary>
    /// rerolls the current upgrades
    /// </summary>
    /// <param name="rerollPrice"> cost of the reroll </param>
    public void Reroll()
    {
        if (PlayerResources.Instance.materials >= currentRerollPrice + rerollIncrease)
        {
            currentRerollPrice += rerollIncrease;
            PlayerResources.Instance.materials -= currentRerollPrice;
            ChooseUpgrades();
        }
    }

    /// <summary>
    /// call this function when opening the upgrades panel
    /// </summary>
    public void OpenUpgradePanel()
    {
        CalculateRerollIncrease();
        CalcChanceThresholds();
        ChooseUpgrades();
    }

    /// <summary>
    /// calculates the current waves reroll price increase
    /// call every time a wave is complete
    /// </summary>
    public void CalculateRerollIncrease()
    {
        rerollIncrease = Mathf.FloorToInt(0.5f * WaveSpawner.Instance.currentWaveIndex);
        rerollIncrease = (rerollIncrease < 1) ? 1 : rerollIncrease;

        currentRerollPrice = WaveSpawner.Instance.currentWaveIndex;
    }


}
