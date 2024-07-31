using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlots : MonoBehaviour
{
    public GameObject item;

    public Image icon;
    public TextMeshProUGUI t_itemName;
    public TextMeshProUGUI t_itemClassifications;
    public TextMeshProUGUI t_itemCost;
    public TextMeshProUGUI t_itemDescription;

    public void SetShopSlot()
    {

    }

    public void BuyItem()
    {
        // if the player has enough money
        ShopManager.Instance.slotAvailability[gameObject] = true;
        gameObject.SetActive(false);
    }
}
