using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public Upgrades[] Heart;
    public Upgrades[] Lungs;
    public Upgrades[] Teeth;
    public Upgrades[] Triceps;
    public Upgrades[] Forearms;
    public Upgrades[] Shoulders;
    public Upgrades[] Brain;
    public Upgrades[] Reflexes;
    public Upgrades[] Fingers;
    public Upgrades[] Skull;
    public Upgrades[] Eyes;
    public Upgrades[] Chest;
    public Upgrades[] Back;
    public Upgrades[] Legs;
    public Upgrades[] Nose;
    public Upgrades[] Hands;

    /// <summary>
    /// Applys the upgrade to the player
    /// </summary>
    /// <param name="upgrade"> the selected upgrade</param>
    public void ApplyUpgrade(Upgrades upgrade) => PlayerBase.Instance.UpdateStat(upgrade.stats, upgrade.amount);
}
