using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public PowerUp powerUpPrefab;
    public LevelManager levelManager;
    public FXManager fxManager;
    private List<PowerUp> _activePowerUps = new List<PowerUp>();

    public void SpawnPowerUp(Vector3 pos)
    {
        var newPowerUp = Instantiate(powerUpPrefab);
        newPowerUp.transform.position = pos;
        _activePowerUps.Add(newPowerUp);
    }

    public void PowerUpCollected(PowerUp powerUp)
    {
        levelManager.PowerUpCollected(powerUp.transform.position);
        fxManager.PlayCoinCollectPS(powerUp.transform.position);
        _activePowerUps.Remove(powerUp);
    }

    public void DestroyActivePowerUps()
    {
        foreach (var powerUp in _activePowerUps)
        {
            Destroy(powerUp.gameObject);
        }
        _activePowerUps.Clear();
    }

    public void PowerUpDestroyed(PowerUp powerUp)
    {
        _activePowerUps.Remove(powerUp);
    }
}
