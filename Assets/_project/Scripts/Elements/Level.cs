using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Brick brickPrefab;
    private List<Brick> _bricks = new List<Brick>();
    private LevelManager _levelManager;
    private FXManager _fxManager;
    public Tile tilePrefab;
    private List<Tile> _availabelTiles = new List<Tile>();
    public void StartLevel(LevelManager levelManager)
    {
        _levelManager = levelManager;
        _fxManager = _levelManager.gameDirector.fxManager;

        GenerateAvailableTiles();
        var brickCount = 1;
        if (_levelManager.currentLevelNo < 11)
        {
            brickCount = _levelManager.currentLevelNo;
        }
        else
        {
            var diff = _levelManager.currentLevelNo - 10;
            brickCount = 10 + diff / 2;
        }
        brickCount = System.Math.Min(brickCount, 20);
        GenerateBricks(brickCount);
    }

    private void GenerateAvailableTiles()
    {
        var xStep = 1f;
        var yStep = 1f;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                var newTile = Instantiate(tilePrefab, transform);
                newTile.transform.localPosition = new Vector3(-2f + xStep * j, -.5f + i * yStep, 0);
                _availabelTiles.Add(newTile);
            }
        }
    }

    private void GenerateBricks(int brickCount)
    {
        var state = Random.state;
        Random.InitState(_levelManager.currentLevelNo);
        for (int i = 0; i < brickCount; i++)
        {
            var newBrick = Instantiate(brickPrefab, transform);
            var xPosRandomizer = Random.Range(-1, 2);
            newBrick.transform.localPosition = SelectFromAvailableTiles();
            _bricks.Add(newBrick);
            newBrick.StartBrick(this, _levelManager);
        }
        Random.state = state;
    }

    Vector3 SelectFromAvailableTiles()
    {
        var selectedTile = _availabelTiles[Random.Range(0, _availabelTiles.Count)];
        _availabelTiles.Remove(selectedTile);
        return selectedTile.transform.localPosition;
    }
    public void BrickDestroyed(Brick brick)
    {
        _bricks.Remove(brick);
        _fxManager.PlayBrickDestroyedParticles(brick.transform.position);
        if (_bricks.Count == 0) _levelManager.LevelCompleted();
        else
        {
            var randomValue = Random.value;
            if (randomValue < .5f) _levelManager.gameDirector.coinManager.CreateCoinPosition(brick.transform.position);
            else if(randomValue < .6f) _levelManager.gameDirector.powerUpManager.SpawnPowerUp(brick.transform.position);
        }

    }
}

