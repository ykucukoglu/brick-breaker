using System;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public Player player;
    public UIManager uiManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) RestartLevel();
        if (Input.GetKeyDown(KeyCode.E)) LoadNextLevel();
        if (Input.GetKeyDown(KeyCode.Q)) LoadPreviousLevel();
    }

    public void Start()
    {
        uiManager.GameStarted();
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
    }

    public void Win()
    {
        levelManager.SetBallDirection(Vector3.zero);
        levelManager.HideBall();
        uiManager.LevelCompleted();
    }

    public void Lose()
    {
        levelManager.SetBallDirection(Vector3.zero);
        levelManager.HideBall();
        uiManager.LevelFailed();
    }
}
