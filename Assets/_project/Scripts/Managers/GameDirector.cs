using System;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public Player player;
    public UIManager uiManager;
    public FXManager fxManager;
    public AudioManager audioManager;
    public CoinManager coinManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) RestartLevel();
        if (Input.GetKeyDown(KeyCode.E)) LoadNextLevel();
        if (Input.GetKeyDown(KeyCode.Q)) LoadPreviousLevel();
    }

    public void Start()
    {
        uiManager.GameStarted();
        LoadPersistanceData();
    }

    private void LoadPersistanceData()
    {
        levelManager.currentLevelNo = Math.Max(PlayerPrefs.GetInt("LevelNo"), 1);
        coinManager.cointCount = PlayerPrefs.GetInt("CoinCount");
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
        levelManager.RestartLevelManager();
        player.RestartPlayer();
        uiManager.ShowInGameUI(levelManager.currentLevelNo);
        coinManager.StartCoinSpawnCoroutine();
    }

    public void Win()
    {
        PlayerPrefs.SetInt("LevelNo", levelManager.currentLevelNo + 1);
        levelManager.SetBallDirection(Vector3.zero);
        levelManager.HideBall();
        uiManager.LevelCompleted();
        coinManager.StopCoinSpawnCoroutine();
    }

    public void Lose()
    {
        audioManager.PlayFailAS();
        levelManager.SetBallDirection(Vector3.zero);
        levelManager.HideBall();
        uiManager.LevelFailed();
        coinManager.StopCoinSpawnCoroutine();
    }
}
