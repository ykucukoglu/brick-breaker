using DG.Tweening;
using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Level _level;
    public int startHealth;
    private int _currentHealth;
    private Color _stepValue;
    public SpriteRenderer sprite;
    public float colorStep;
    private AudioManager _audioManager;
    private IncrementalManager _incrementalManager;

    public void StartBrick(Level level, LevelManager levelManager)
    {
        if (levelManager.currentLevelNo > 20 && levelManager.currentLevelNo < 31) startHealth += 1;
        else if (levelManager.currentLevelNo > 30) startHealth += 2;

        _level = level;
        _currentHealth = startHealth;
        _incrementalManager = levelManager.gameDirector.incrementalManager;
        var greenandblueValue = 1 - _currentHealth * colorStep;
        sprite.color = new Color(1, greenandblueValue, greenandblueValue, 1);
        _audioManager = levelManager.gameDirector.audioManager;
    }

    public void GetHit()
    {
        var totalDamage = 1 + _incrementalManager.GetDamageUpgradeCount();
        _currentHealth -= totalDamage;
        var greenandblueValue = 1 - _currentHealth * colorStep;

        PlayVisualFX(greenandblueValue);
        if (_currentHealth <= 0) DestroyBrick();
    }

    private void PlayVisualFX(float greenandblueValue)
    {
        sprite.transform.DOKill();
        sprite.transform.localScale = Vector3.one * .2f;
        sprite.transform.localPosition = Vector3.zero;
        sprite.transform.DOScale(.23f, .05f).SetLoops(2, LoopType.Yoyo);
        sprite.DOColor(new Color(1, greenandblueValue, greenandblueValue, 1), .1f);
        sprite.transform.DOPunchPosition(Vector3.one * .1f, .1f, 100);
    }

    private void DestroyBrick()
    {
        gameObject.SetActive(false);
        _level.BrickDestroyed(this);
        _audioManager.PlayExplodeAS();
    }

    private void OnDestroy()
    {
        sprite.transform.DOKill();
        sprite.DOKill();
    }
}
