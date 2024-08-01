using Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlots : MonoBehaviour
{
    public GameObject weapon;
    public GameObject detailPanel;

    [SerializeField] private Image invSlotPanel;
    [SerializeField] private Image icon;

    [SerializeField] private Image invDetailPanel;
    [SerializeField] private Image invDetailIcon;
    [SerializeField] private TextMeshProUGUI invDetailDesc;
    [SerializeField] private TextMeshProUGUI invDetailName;
    [SerializeField] private TextMeshProUGUI invDetailClassification;
    [SerializeField] private TextMeshProUGUI invDetailSellPrice;

    [SerializeField] private Color[] colors;
    public void SetInventorySlot(GameObject invWeapon)
    {
        weapon = invWeapon;
        int tier = (int)weapon.GetComponent<WeaponBehaviour>().CurrentTier;
        

        icon.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        invSlotPanel.color = colors[tier];

        invDetailIcon.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        invDetailPanel.color = colors[tier];
        invDetailDesc.text = GenerateItemDescription();
        invDetailName.text = weapon.name;
        invDetailClassification.text = weapon.GetComponent<WeaponBehaviour>().WeaponData.WeaponClass.ToString();
        invDetailSellPrice.text = "$---";
    }

    public string GenerateItemDescription()
    {
        Weapon itemData = weapon.GetComponent<WeaponBehaviour>().WeaponData;


        List<string> description = new List<string>();
        int tier = (int)weapon.GetComponent<WeaponBehaviour>().CurrentTier;

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

}
