using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeGameState : BaseGameState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Upgrade State");
        manager.upgradeUI.SetActive(true);
        UpgradeManager.Instance.OpenUpgradePanel();
    }

    public override void ExitState(GameManager manager)
    {
        manager.upgradeUI.SetActive(false);
        manager.ResumeGame();
    }

    public override void UpdateState(GameManager manager)
    {
        // Update logic for Upgrade State if needed
    }
}
