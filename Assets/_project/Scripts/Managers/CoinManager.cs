using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public AudioManager audioManager;
    public Coin coinPrefab;
    public int cointCount;
    private Coroutine _coinSpawnCoroutine;
    public CoinUI coinUI;
    public FXManager fxManager;
    private List<Coin> _activeCoins = new List<Coin>();
    public void StartCoinSpawnCoroutine()
    {
        _coinSpawnCoroutine = StartCoroutine(CoinSpawnCoroutine());
    }

    public void StopCoinSpawnCoroutine()
    {
        if (_coinSpawnCoroutine != null)
        {
            StopCoroutine(_coinSpawnCoroutine);
            _coinSpawnCoroutine = null;
        }
    }

    IEnumerator CoinSpawnCoroutine()
    {
        while (true)
        {
            var spawnTime = Random.Range(3f, 6f);
            var spawnPos = new Vector3(Random.Range(-2.3f, 2.3f), Random.Range(0, 4.5f), 0);
            CreateCoinPosition(spawnPos);
            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void CreateCoinPosition(Vector3 pos)
    {
        var newCoin = Instantiate(coinPrefab);
        newCoin.transform.position = pos;
        newCoin.transform.localScale = Vector3.zero;
        newCoin.transform.DOScale(1, .2f).SetEase(Ease.OutBack);
        _activeCoins.Add(newCoin);
    }

    public void CoinCollected(Coin coin)
    {
        cointCount++;
        coinUI.UpdateCoinCount(cointCount);
        audioManager.PlayCoinCollectAS();
        PlayerPrefs.SetInt("CoinCount", cointCount);
        fxManager.PlayCoinCollectPS(coin.transform.position);
        _activeCoins.Remove(coin);
    }

    public void SpendCoins(int spendAmount)
    {
        cointCount -= spendAmount;
        coinUI.UpdateCoinCount(cointCount);
        PlayerPrefs.SetInt("CoinCount", cointCount);
    }

    public void DestroyActiveCoins()
    {
        foreach (var coin in _activeCoins)
        {
            Destroy(coin.gameObject);
        }
        _activeCoins.Clear();
    }

    public void CoinDestroyed(Coin coin)
    {
        _activeCoins.Remove(coin);
    }

    public void ResetCoinCount()
    {
        cointCount = 0;
    }
}
