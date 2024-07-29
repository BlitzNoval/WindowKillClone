using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGameState : BaseGameState
{
    public override void EnterState(GameManager manager)
    {
        //open the shop and run logic here
        Debug.Log("Shop State");
        manager.SwitchState(manager.playState);
    }

    public override void ExitState(GameManager manager)
    {

    }

    public override void UpdateState(GameManager manager)
    {

    }
}
