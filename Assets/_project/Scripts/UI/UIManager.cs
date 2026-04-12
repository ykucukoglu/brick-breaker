using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public MainMenu mainMenu;
    public WinUI winUI;
    public LoseUI loseUI;

    public LevelUI levelUI;

    public void GameStarted()
    {
        mainMenu.Show();
        winUI.Hide();
        loseUI.Hide();
        HideInGameUI();
    }

    public void ShowInGameUI(int levelNo)
    {
        levelUI.Show(levelNo);
    }

    public void HideInGameUI()
    {
        levelUI.Hide();
    }

    public void PlayGameButtonPressed()
    {
        gameDirector.RestartLevel();
    }

    public void LevelCompleted()
    {
        winUI.Show(.5f);
        HideInGameUI();
    }

    public void LevelFailed()
    {
        loseUI.Show(.5f);
        HideInGameUI();
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
