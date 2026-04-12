using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public MainMenu mainMenu;
    public WinUI winUI;
    public LoseUI loseUI;

    public void GameStarted()
    {
        mainMenu.Show();
        winUI.Hide();
        loseUI.Hide();
    }

    public void PlayGameButtonPressed()
    {
        gameDirector.RestartLevel();
    }

    public void LevelCompleted()
    {
        winUI.Show(.5f);
    }

    public void LevelFailed()
    {
        loseUI.Show(.5f);
    }

    public void LoadNextLevelButtonPressed()
    {
        gameDirector.LoadNextLevel();
    }

    public void RestartLevelButtonPressed()
    {
        gameDirector.RestartLevel();
    }
}
