using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public List<Level> levels;
    public Ball ballPrefab;
    public int currentLevelNo;
    private Level _currentLevel;
    private Ball _currentBall;

    public void RestartLevelManager()
    {
        DeletePreviousLevel();
        CreateNewLevel();
        DeletePreviousBall();
        CreateNewBall();
    }

    private void CreateNewBall()
    {
        _currentBall = Instantiate(ballPrefab);
        _currentBall.transform.position = new Vector3(0, -3f, 0);
        _currentBall.StartBall(new Vector3(Random.Range(-1f, 1f), 1, 0));
    }

    private void DeletePreviousBall()
    {
        if (_currentBall != null)
        {
            Destroy(_currentBall.gameObject);
        }
    }

    private void CreateNewLevel()
    {
        var normalizedLevelNo = (currentLevelNo - 1) % levels.Count;
        _currentLevel = Instantiate(levels[normalizedLevelNo]);
        _currentLevel.transform.position = Vector3.zero;
        _currentLevel.StartLevel(this);
    }

    private void DeletePreviousLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel.gameObject);
        }
    }

    public void LevelCompleted()
    {
        _currentBall.SetBallDireciton(Vector3.zero);
        gameDirector.Win();
    }

    public void SetBallDirection(Vector3 dir)
    {
        _currentBall.SetBallDireciton(dir);
    }

    public void HideBall()
    {
        _currentBall.transform.DOScale(0, .2f).SetEase(Ease.InBack);
    }
}
