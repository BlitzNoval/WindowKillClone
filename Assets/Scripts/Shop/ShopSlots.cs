using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlots : MonoBehaviour
{
    public GameObject item;
    public Image panel;
    public Color[] tierColours;

    public WeaponTier itemTier;

    public Image icon;
    public TextMeshProUGUI t_itemName;
    public TextMeshProUGUI t_itemClassifications;
    public TextMeshProUGUI t_itemCost;
    public TextMeshProUGUI t_itemDescription;

    [SerializeField] private int itemCost;

    [SerializeField] private bool itemLocked;

    private void Start()
    {
        // for debugging
        SetShopSlot(itemTier);
    }

    public void SetShopSlot(WeaponTier tier)
    {
        itemTier = tier;
        WeaponBehaviour itemBehaviour = item.GetComponent<WeaponBehaviour>();

        itemBehaviour.CurrentTier = itemTier;
        t_itemName.text = item.name;
        t_itemClassifications.text = itemBehaviour.WeaponData.WeaponClass.ToString();
        t_itemDescription.text = GenerateItemDescription();
        t_itemCost.text = $"$ {ShopManager.Instance.CalculateInflation(itemBehaviour.WeaponData.BasePricePerTier[(int)tier])}";

        panel.color = tierColours[(int)tier];

        itemCost = ShopManager.Instance.CalculateInflation(itemBehaviour.WeaponData.BasePricePerTier[(int)tier]);

        //get informaiton and plug it in here
    }

    public string GenerateItemDescription()
    {
        Weapon itemData = item.GetComponent<WeaponBehaviour>().WeaponData;


        List<string> description = new List<string>();
        int tier = (int)itemTier;

        //Damage
        description.Add($"Damage : {itemData.DamagePerTier[tier]}");

        //Critical
        description.Add($"Critical : {itemData.CritDamagePerTier[tier]} ({itemData.CritChancePerTier[tier] * 100}% chance)");

        //Cooldown
        description.Add($"Cooldown : {itemData.AttackSpeedPerTier[tier]}");

        //Knockback
        if (itemData.KnockbackPerTier[tier] != 0)
        {
            description.Add($"Knockback : {itemData.KnockbackPerTier[tier]}");
        }

        //Range
        description.Add($"Range : {itemData.RangePerTier[tier]} ({itemData.WeaponType})");

        //Lifesteal
        if (itemData.LifestealPerTier[tier] != 0)
        {
            description.Add($"Lifesteal : {itemData.LifestealPerTier[tier]}%");
        }

        return string.Join("\n", description);
    }

    public void BuyItem()
    {
        // if the player has enough money

        //add to playerInventory
        if (PlayerResources.Instance.materials >= itemCost)
        {
            //spawn the item and pass check if it can be added
            GameObject new_item = Instantiate(item);


            if (WeaponController.Instance.IsAddable(new_item))
            {
                PlayerResources.Instance.materials -= itemCost;
                WeaponController.Instance.AddWeapon(new_item);

                ShopManager.Instance.slotAvailability[gameObject] = true;
                gameObject.SetActive(false);
            }
        }
    }

    public void LockItem()
    {
        if (itemLocked)
        {
            //make the slot available (to be rerolled)
            ShopManager.Instance.slotAvailability[gameObject] = true;
        }
        else
        {
            //lock the slot
            ShopManager.Instance.slotAvailability[gameObject] = false;
        }
    }

}
