using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    public GameObject[] allItems = new GameObject[0];
    public float[] chanceThresholds = new float[4];
    public ShopScale[] shopScales = new ShopScale[4];

    public GameObject[] shopSlots = new GameObject[4];

    public Dictionary<GameObject, bool> slotAvailability = new Dictionary<GameObject, bool>();

    public TextMeshProUGUI rerollButton;

    public int currentRerollPrice;
    public int rerollIncrease;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        foreach (GameObject slot in shopSlots)
        {
            slotAvailability.Add(slot, true);
        }
    }

    private void ChooseShopItems()
    {
        GameObject[] selectedItems = RandomItems();

        for (int i = 0; i < shopSlots.Length; i++)
        {
            // if the slot is available in the dictionary (key, value)
            if (slotAvailability[shopSlots[i]])
            {
                GameObject item = selectedItems[i];
                shopSlots[i].GetComponent<ShopSlots>().item = item;

                //generate a random number and check if it is above the rarity threshold
                float randNum = Random.Range(0, 1f);

                if (randNum > chanceThresholds[1])
                {
                    shopSlots[i].GetComponent<ShopSlots>().SetShopSlot(WeaponTier.Common);
                }
                else if (randNum > chanceThresholds[2])
                {
                    shopSlots[i].GetComponent<ShopSlots>().SetShopSlot(WeaponTier.Uncommon);
                }
                else if (randNum > chanceThresholds[3])
                {
                    shopSlots[i].GetComponent<ShopSlots>().SetShopSlot(WeaponTier.Rare);
                }
                else
                {
                    shopSlots[i].GetComponent<ShopSlots>().SetShopSlot(WeaponTier.Legendary);
                }

                slotAvailability[shopSlots[i]] = false;
            }
        }
    }

    private GameObject[] RandomItems()
    {
        List<GameObject> l_items = allItems.ToList();
        GameObject[] selectedItems;
        if (l_items.Count <4)
        {
            selectedItems = new GameObject[l_items.Count];
        } else
        {
            selectedItems = new GameObject[4];
        }
        //GameObject[] selectedItems = new GameObject[4];

        for (int i = 0; i < selectedItems.Length; i++)
        {
            selectedItems[i] = l_items[Random.Range(0, l_items.Count)];
            l_items.Remove(selectedItems[i]);
        }

        return selectedItems;
    }
    private void CalcChanceThresholds()
    {
        for (int i = 0; i < chanceThresholds.Length; i++)
        {
            int minLevel = shopScales[i].minWave;
            float baseChance = shopScales[i].baseChance;
            float chancePerWave = shopScales[i].chancePerWave;
            float maxChance = shopScales[i].maxChance;

            float newThreshold = ((chancePerWave * (WaveSpawner.Instance.currentWaveIndex - minLevel)) + baseChance) * (1 + PlayerBase.Instance.primaryStats.luck / 100);
            newThreshold = Mathf.Clamp(newThreshold, 0, maxChance);

            chanceThresholds[i] = newThreshold;
        }
    }

    public void OpenShopPanel()
    {
        CalculateRerollIncrease();
        CalcChanceThresholds();
        ChooseShopItems();
    }

    public void Reroll()
    {
        if (PlayerResources.Instance.materials >= currentRerollPrice + rerollIncrease)
        {
            PlayerResources.Instance.materials -= currentRerollPrice;
            currentRerollPrice += rerollIncrease;
            // reroll the shop
            rerollButton.text = $"Reroll (${currentRerollPrice})";
        }
    }

    public void CalculateRerollIncrease()
    {
        rerollIncrease = Mathf.FloorToInt(0.5f * WaveSpawner.Instance.currentWaveIndex);
        rerollIncrease = (rerollIncrease < 1) ? 1 : rerollIncrease;

        currentRerollPrice = WaveSpawner.Instance.currentWaveIndex;

        rerollButton.text = $"Reroll (${currentRerollPrice})";
    }

    public int CalculateInflation(int basePrice)
    {
        Debug.Log(Mathf.RoundToInt(basePrice + (WaveSpawner.Instance.currentWaveIndex + 1) + (basePrice * 0.1f * (WaveSpawner.Instance.currentWaveIndex + 1))));
        return Mathf.RoundToInt(basePrice + (WaveSpawner.Instance.currentWaveIndex + 1) + (basePrice * 0.1f * (WaveSpawner.Instance.currentWaveIndex + 1)));
    }
}
