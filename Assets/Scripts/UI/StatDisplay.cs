using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{
    public static StatDisplay Instance { get; private set; }
    public GameObject[] statColumns;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpgradteStatColumns();
    }

    public void UpgradteStatColumns()
    {
        foreach (var col in statColumns)
        {
            col.GetComponent<StatCol>().SetStat();
        }
    }
}
