using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGameState : BaseGameState
{
    public override void EnterState(GameManager manager)
    {
        //open the shop and run logic here
        Debug.Log("Shop State");
        manager.shopUI.SetActive(true);

        ShopManager.Instance.OpenShopPanel();
        InventoryManager.Instance.UpdateSlots();
        //manager.SwitchState(manager.playState);
    }

    public override void ExitState(GameManager manager)
    {
        manager.ResumeGame();
    }

    public override void UpdateState(GameManager manager)
    {

    }
}
