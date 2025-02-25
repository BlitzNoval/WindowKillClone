using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public GameObject[] inventorySlots = new GameObject[6];

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < WeaponController.Instance.WeaponInventory.Count)
            {
                GameObject weapon = WeaponController.Instance.WeaponInventory[i];
                inventorySlots[i].GetComponent<InventorySlots>().SetInventorySlot(weapon);
            }
            else
            {
                inventorySlots[i].GetComponent<InventorySlots>().SetInventorySlot(null);
            }
        }
    }
}
