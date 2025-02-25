using UnityEngine;

public class PlayGameState : BaseGameState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Play State");
        manager.upgradeUI.SetActive(false);
        manager.staticCanvas.SetActive(false);
        WaveSpawner.Instance.StartNextWave();

        manager.lubaUI.LevelRestart();
    }

    public override void ExitState(GameManager manager)
    {
        manager.PauseGame();
        manager.upgradeUI.SetActive(true);
        UpgradeManager.Instance.CalculateRerollIncrease();
    }

    public override void UpdateState(GameManager manager)
    {
        // Check if the wave is active
        if (!WaveSpawner.Instance.IsWaveActive)
        {
            manager.SwitchState(manager.upgradeState);
        }
    }
}
