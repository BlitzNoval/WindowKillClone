using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject[] groupsArr;

    [SerializeField] private GameObject currentGroup;

    [SerializeField] private List<GameObject> weaponInventory;

    private int inventorySize = 6;

    private void Awake()
    {
        DoInventoryUpdate();
    }

    /// <summary>
    /// Updates current group based on the size of the inventory
    /// </summary>
    public void DoInventoryUpdate()
    {
        currentGroup = groupsArr[Mathf.Clamp(weaponInventory.Count - 1, 0, 5)];
        PlaceWeapons();
    }

    /// <summary>
    /// Places weapons around the character based on the locations of the weapon plugs
    /// </summary>
    private void PlaceWeapons()
    {
        for (int i = 0; i < weaponInventory.Count; i++)
        {
            weaponInventory[i].transform.position = currentGroup.transform.GetChild(i).transform.position;
        }
    }

    /// <summary>
    /// Add a weapon to the player inventory
    /// </summary>
    /// <param name="weaponToAdd"> An instance of the prefab to add </param>
    public void AddWeapon(GameObject weaponToAdd)
    {
        
    }

    /// <summary>
    /// Test if a weapon can be added to the inventory. Returns true if the weapon can merge or the inventory is not full
    /// </summary>
    /// <param name="weaponToAdd"> An instance of the prefab to add </param>
    public bool CanAddWeapon(GameObject checkingWeapon)
    {
        if (weaponInventory.Count < inventorySize) return true;
        //todo add logic to test mergeability
        return false;
    }
}
