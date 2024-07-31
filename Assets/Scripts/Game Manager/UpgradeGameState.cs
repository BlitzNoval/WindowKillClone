using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeGameState : BaseGameState
{
    public override void EnterState(GameManager manager)
    {
        //open the Upgrade UI
        Debug.Log("Update State");



        manager.staticCanvas.SetActive(true);
        manager.upgradeUI.SetActive(true);
        manager.shopUI.SetActive(true);

        UpgradeManager.Instance.OpenUpgradePanel();
    }

    public override void ExitState(GameManager manager)
    {
        manager.upgradeUI.SetActive(false);
        manager.ResumeGame();
    }

    public override void UpdateState(GameManager manager)
    {
        
    }
}

