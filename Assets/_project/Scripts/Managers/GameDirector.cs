using System;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public IncrementalManager incrementalManager;
    public Player player;
    public UIManager uiManager;
    public FXManager fxManager;
    public AudioManager audioManager;
    public CoinManager coinManager;
    public PowerUpManager powerUpManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) RestartLevel();
        if (Input.GetKeyDown(KeyCode.E)) LoadNextLevel();
        if (Input.GetKeyDown(KeyCode.Q)) LoadPreviousLevel();
    }

    public void Start()
    {
        LoadPersistanceData();
        uiManager.GameStarted();
    }

    private void LoadPersistanceData()
    {
        levelManager.currentLevelNo = Math.Max(PlayerPrefs.GetInt("LevelNo"), 1);
        coinManager.cointCount = PlayerPrefs.GetInt("CoinCount");
        incrementalManager.LoadPersistanceData();
    }
    public void LoadNextLevel()
    {
        levelManager.currentLevelNo++;
        RestartLevel();
    }

    private void LoadPreviousLevel()
    {
        levelManager.currentLevelNo = Mathf.Max(levelManager.currentLevelNo - 1, 1);
        RestartLevel();
    }

    public void RestartLevel()
    {
        coinManager.StopCoinSpawnCoroutine();
        levelManager.RestartLevelManager();
        player.RestartPlayer();
        uiManager.ShowInGameUI(levelManager.currentLevelNo);
        coinManager.StartCoinSpawnCoroutine();
        audioManager.StartMusic(levelManager.currentLevelNo);
        fxManager.ChangeBackground(levelManager.currentLevelNo);
    }

    public void Win()
    {
        PlayerPrefs.SetInt("LevelNo", levelManager.currentLevelNo + 1);
        levelManager.SetBallsDirection(Vector3.zero);
        levelManager.HideBalls();
        uiManager.LevelCompleted();
        coinManager.StopCoinSpawnCoroutine();
        coinManager.DestroyActiveCoins();
        powerUpManager.DestroyActivePowerUps();
        audioManager.StopMusic();
    }

    public void Lose()
    {
        audioManager.PlayFailAS();
        levelManager.SetBallsDirection(Vector3.zero);
        levelManager.HideBalls();
        uiManager.LevelFailed();
        coinManager.StopCoinSpawnCoroutine();
        coinManager.DestroyActiveCoins();
        powerUpManager.DestroyActivePowerUps();
        audioManager.StopMusic();
    }
}
