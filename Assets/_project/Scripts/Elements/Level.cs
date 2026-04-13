using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    private List<Brick> _bricks;
    private LevelManager _levelManager;
    private FXManager _fxManager;
    public void StartLevel(LevelManager levelManager)
    {
        _levelManager = levelManager;
        _fxManager = _levelManager.gameDirector.fxManager;
        _bricks = GetComponentsInChildren<Brick>().ToList();
        foreach(var brick in _bricks)
        {
            brick.StartBrick(this);
        }
    }

    public void BrickDestroyed(Brick brick)
    {
        _bricks.Remove(brick);
        _fxManager.PlayBrickDestroyedParticles(brick.transform.position);
        if (_bricks.Count == 0)
        {
            _levelManager.LevelCompleted();
        }
    }
}

