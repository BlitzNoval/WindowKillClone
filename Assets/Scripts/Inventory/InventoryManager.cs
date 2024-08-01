using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public GameObject[] inventorySlots;


    private void Awake()
    {
        Instance = this;
    }
}
