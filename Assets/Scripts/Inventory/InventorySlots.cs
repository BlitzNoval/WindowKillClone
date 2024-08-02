using Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    [SerializeField] private GameObject invDetailMerge;

    [SerializeField] private Color[] colors;
    public void SetInventorySlot(GameObject invWeapon)
    {
        weapon = invWeapon;

        if (weapon != null)
        {
            int tier = (int)weapon.GetComponent<WeaponBehaviour>().CurrentTier;
            icon.enabled = true;
            icon.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
            invSlotPanel.color = colors[tier];
            icon.color = Color.white;

            invDetailIcon.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
            invDetailPanel.color = colors[tier];
            invDetailDesc.text = GenerateItemDescription();
            invDetailName.text = weapon.name;
            invDetailClassification.text = weapon.GetComponent<WeaponBehaviour>().WeaponData.WeaponClass.ToString();
            invDetailSellPrice.text = $"Recycle ${weapon.GetComponent<WeaponBehaviour>().WeaponData.BasePricePerTier[(int)weapon.GetComponent<WeaponBehaviour>().CurrentTier]}";

        }
        else
        {
            icon.color = colors[0];
            icon.enabled = false;
            invSlotPanel.color = colors[0];
        }

        if (weapon != null)
        {
            if (WeaponController.Instance.IsMergeable(weapon))
            {
                invDetailMerge.SetActive(true);
            }
            else
            {
                invDetailMerge.SetActive(false);
            }
        }
        else
        {
            invDetailMerge.SetActive(false);
        }
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

    public void SellItem()
    {
        PlayerResources.Instance.materials += (weapon.GetComponent<WeaponBehaviour>().WeaponData.BasePricePerTier[(int)weapon.GetComponent<WeaponBehaviour>().CurrentTier]) / 4;
        WeaponController.Instance.RemoveWeapon(weapon);
        weapon = null;
        InventoryManager.Instance.UpdateSlots();
    }

    public void MergeItem()
    {
        WeaponController.Instance.DoMerge(weapon);
        InventoryManager.Instance.UpdateSlots();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (weapon != null)
        {
            detailPanel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        detailPanel.SetActive(false);
    }
}
