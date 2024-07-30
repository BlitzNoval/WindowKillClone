using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject upgradeUI;

    public BaseGameState currentState;
    public UpgradeGameState upgradeState = new UpgradeGameState();
    public ShopGameState shopState = new ShopGameState();
    public PlayGameState playState = new PlayGameState();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        currentState = upgradeState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseGameState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}

public abstract class BaseGameState
{
    public abstract void EnterState(GameManager manager);

    public abstract void ExitState(GameManager manager);

    public abstract void UpdateState(GameManager manager);
}