using System;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public BrickManager brickManager;
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
        brickManager.RestartBrickManager();
        player.RestartPlayer();
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
        Invoke(nameof(RestartLevel), 1f);
    }
}
