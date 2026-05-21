using System;
using UnityEngine;

public class IncrementalManager : MonoBehaviour
{
    private int _damageUpgradeCount;
    public CoinManager coinManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) {
            if(GetDamageUpgradeCost() <= coinManager.cointCount)
            {
                coinManager.SpendCoins(GetDamageUpgradeCost());
                UpgradeDamage();
            }
        }
        if (Input.GetKeyDown(KeyCode.V)) ResetDamageUpgrade();
    }

    private void ResetDamageUpgrade()
    {
        _damageUpgradeCount = 0;
        PlayerPrefs.SetInt("DamageUpgrade", _damageUpgradeCount);
    }

    public void UpgradeDamage()
    {
        _damageUpgradeCount++;
        PlayerPrefs.SetInt("DamageUpgrade", _damageUpgradeCount);
    }

    public void LoadPersistanceData()
    {
        _damageUpgradeCount = PlayerPrefs.GetInt("DamageUpgrade");
    }

    public int GetDamageUpgradeCount()
    {
        return _damageUpgradeCount;
    }

    public int GetDamageUpgradeCost()
    {
        return 100 + _damageUpgradeCount * 100;
    }
}
