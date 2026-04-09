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

    public void StartBrick(Level level)
    {
        _level = level;
        _currentHealth = startHealth;
        var greenandblueValue = 1 - _currentHealth * colorStep;
        sprite.color = new Color(1, greenandblueValue, greenandblueValue, 1);
    }

    public void GetHit()
    {
        _currentHealth--;
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
    }


}
