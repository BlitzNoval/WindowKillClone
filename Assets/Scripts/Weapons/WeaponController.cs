using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject testAddPrefab;
    [SerializeField] private GameObject[] groupsArr;

    [SerializeField] private GameObject currentGroup;

    [SerializeField] private List<GameObject> weaponInventory;

    private int maxInventorySize = 6;

    private void Awake()
    {
        DoInventoryUpdate();
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject test = Instantiate(testAddPrefab, transform);
            if (IsAddable(test))
            {
                AddWeapon(test);
            }
            else
            {
                Destroy(test);
            }
        }
        */
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
        if (weaponInventory.Count < maxInventorySize)
        {
            weaponInventory.Add(weaponToAdd);
        } 
        else if (IsMergeable(weaponToAdd))
        {
            weaponInventory.Add(weaponToAdd);
            DoMerge(weaponToAdd);
        }
        DoInventoryUpdate();
    }

    /// <summary>
    /// Remove a weapon from the player inventory
    /// </summary>
    /// <param name="weaponToRemove"> The gameObject to be removed from the inventory </param>
    public void RemoveWeapon(GameObject weaponToRemove)
    {
        weaponInventory.Remove(weaponToRemove);
        Destroy(weaponToRemove);
        DoInventoryUpdate();
    }

    /// <summary>
    /// Test if a weapon can be added to the inventory.
    /// </summary>
    /// <param name="checkingWeapon"> An instance of the prefab to check against </param>
    /// <returns> Returns true if the weapon can merge or the inventory is not full </returns>
    public bool IsAddable(GameObject checkingWeapon)
    {
        if (weaponInventory.Count < maxInventorySize) return true;
        if (IsMergeable(checkingWeapon)) return true;
        return false;
    }

    /// <summary>
    /// Test if a weapon can be merged with another in the inventory
    /// </summary>
    /// <param name="testWeapon"> The weapon that will be checked </param>
    /// <returns> Returns true if the weapon is able to be merged </returns>
    private bool IsMergeable(GameObject testWeapon)
    {
        //Getting the behaviour script
        WeaponBehaviour baseBehaviour = testWeapon.GetComponent<WeaponBehaviour>();
        
        //Getting important components from the behaviour
        WeaponTier baseTier = baseBehaviour.CurrentTier;
        string baseID = baseBehaviour.WeaponData.WeaponID;

        //Always returns false if a weapon is Legendary
        if (baseTier == WeaponTier.Legendary) return false;

        foreach (var weapon in weaponInventory)
        {
            //Skips loop execution if the tester is the same as the input weapon
            if (weapon == testWeapon) continue;
            
            WeaponBehaviour testBehaviour = weapon.GetComponent<WeaponBehaviour>();
            if (testBehaviour.CurrentTier == baseTier && testBehaviour.WeaponData.WeaponID == baseID)
            {
                //We have found a weapon that can be merged
                return true;
            }
        }

        return false;
    }
    
    /// <summary>
    /// Upgrades a weapon by merging it with the first weapon it finds of the same ID and tier
    /// </summary>
    /// <param name="weaponToMerge"> The weapon that will be upgraded </param>
    public void DoMerge(GameObject weaponToMerge)
    {
        WeaponBehaviour baseBehaviour = weaponToMerge.GetComponent<WeaponBehaviour>();
        WeaponTier baseTier = baseBehaviour.CurrentTier;
        string baseID = baseBehaviour.WeaponData.WeaponID;

        foreach (var weapon in weaponInventory)
        {
            //Skips loop execution if the tester is the same as the input weapon
            if (weapon == weaponToMerge) continue;
            WeaponBehaviour testBehaviour = weapon.GetComponent<WeaponBehaviour>();
            if (testBehaviour.CurrentTier == baseTier && testBehaviour.WeaponData.WeaponID == baseID)
            {
                //We have found a weapon that can be merged
                RemoveWeapon(weapon);
                DoInventoryUpdate();
                
                //Upgrading the base weapon
                baseBehaviour.CurrentTier++;
                baseBehaviour.UpdateRange();
                break;
            }
        }
    }

    private void UpdateRanges()
    {
        foreach (var weapon in weaponInventory)
        {
            weapon.GetComponent<WeaponBehaviour>().UpdateRange();
        }
    }
}
