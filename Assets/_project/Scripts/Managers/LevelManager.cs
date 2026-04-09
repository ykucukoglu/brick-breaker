using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels;
    public int currentLevelNo;
    private Level _currentLevel;
    public void RestartLevelManager()
    {
        DeletePreviousLevel();
        CreateNewLevel();
    }

    private void CreateNewLevel()
    {
        var normalizedLevelNo = (currentLevelNo-1) % levels.Count;
        _currentLevel = Instantiate(levels[normalizedLevelNo]);
        _currentLevel.transform.position = Vector3.zero;
    }

    private void DeletePreviousLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel.gameObject);
        }
    }
}
