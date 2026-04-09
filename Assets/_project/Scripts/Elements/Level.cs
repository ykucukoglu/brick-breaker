using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    private List<Brick> _bricks;
    private LevelManager _levelManager;
    public void StartLevel(LevelManager levelManager)
    {
        _levelManager = levelManager;
        _bricks = GetComponentsInChildren<Brick>().ToList();
        foreach(var brick in _bricks)
        {
            brick.StartBrick(this);
        }
    }
}

