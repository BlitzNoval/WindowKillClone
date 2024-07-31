using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject staticCanvas;
    public GameObject upgradeUI;
    public GameObject shopUI;

    public GameObject player; // Reference to the player object to disable movement
    public BaseGameState currentState;
    public UpgradeGameState upgradeState = new UpgradeGameState();
    public ShopGameState shopState = new ShopGameState();
    public PlayGameState playState = new PlayGameState();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentState = playState; // Start with the shop state
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false; //
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}


public abstract class BaseGameState
{
    public abstract void EnterState(GameManager manager);

    public abstract void ExitState(GameManager manager);

    public abstract void UpdateState(GameManager manager);
}