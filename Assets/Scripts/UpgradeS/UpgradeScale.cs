using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeScale
{
    public int minLevel;
    public float baseChance;
    public float chancePerLevel;
    public float maxChance;
}

[Serializable]

public class ShopScale
{
    public int minWave;
    public float baseChance;
    public float chancePerWave;
    public float maxChance;
}