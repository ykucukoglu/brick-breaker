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
    private List<Ball> _activeBalls = new List<Ball>();

    public void RestartLevelManager()
    {
        DeletePreviousLevel();
        CreateNewLevel();
        DeletePreviousBalls();
        CreateNewBall(new Vector3(0, -3f, 0));
    }

    private void CreateNewBall(Vector3 pos)
    {
        var newBall = Instantiate(ballPrefab);
        newBall.transform.position = pos;
        newBall.StartBall(this, new Vector3(Random.Range(-1f, 1f), 1, 0));
        _activeBalls.Add(newBall);
    }

    private void DeletePreviousBalls()
    {
        foreach (var ball in _activeBalls)
        {
            Destroy(ball.gameObject);
        }
        _activeBalls.Clear();
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
        foreach(var ball in _activeBalls)
        {
            ball.SetBallDireciton(Vector3.zero);
        }
        gameDirector.Win();
    }

    public void SetBallsDirection(Vector3 dir)
    {
        foreach (var ball in _activeBalls)
        {
            ball.SetBallDireciton(dir);
        }
    }

    public void HideBalls()
    {
        foreach (var ball in _activeBalls)
        {
            ball.transform.DOScale(0, .2f).SetEase(Ease.InBack).OnComplete(() => Destroy(ball.gameObject));
        }
        _activeBalls.Clear();
    }

    public void PowerUpCollected(Vector3 pos)
    {
        CreateNewBall(pos);
    }

    public void BallDestroyed(Ball ball)
    {
        _activeBalls.Remove(ball);
        if (_activeBalls.Count <= 0) gameDirector.Lose();
    }
}
