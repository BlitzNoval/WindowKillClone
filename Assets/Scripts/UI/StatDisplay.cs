using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{
    public GameObject[] statColumns;

    public void UpgradteStatColumns()
    {
        foreach (var col in statColumns)
        {
            col.GetComponent<StatCol>().SetStat();
        }
    }
}
