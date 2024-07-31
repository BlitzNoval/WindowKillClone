using Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlots : MonoBehaviour
{
    public GameObject item;

    public WeaponTier itemTier;

    public Image icon;
    public TextMeshProUGUI t_itemName;
    public TextMeshProUGUI t_itemClassifications;
    public TextMeshProUGUI t_itemCost;
    public TextMeshProUGUI t_itemDescription;

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
        t_itemCost.text = $"$ {itemBehaviour.WeaponData.BasePricePerTier[(int)tier]}";


        //get informaiton and plug it in here
    }

    public void BuyItem()
    {
        // if the player has enough money
        //add to playerInventory
        ShopManager.Instance.slotAvailability[gameObject] = true;
        gameObject.SetActive(false);
    }

    public string GenerateItemDescription()
    {
        Weapon itemData = item.GetComponent<WeaponBehaviour>().WeaponData;


        string description = "";
        int tier = (int)itemTier;

        //Damage
        description += $"Damage : {itemData.DamagePerTier[tier]} \n";

        //Critical
        description += $"Critical : {itemData.CritDamagePerTier[tier]} ({itemData.CritChancePerTier[tier]}% chance) \n";

        //Cooldown
        description += $"Cooldown : {itemData.AttackSpeedPerTier[tier]} \n";

        //Knockback
        description += (itemData.KnockbackPerTier[tier] != 0) ? $"Knockback : {itemData.KnockbackPerTier[tier]} \n" : "";

        //Range
        description += $"Range : {itemData.RangePerTier[tier]} ({itemData.WeaponType}) \n";

        return description;
    }
}
