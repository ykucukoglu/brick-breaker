using DG.Tweening;
using TMPro;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Level _level;

    public Vector2 startHealthRange;

    private int _startHealth;
    private int _currentHealth;
    private Color _stepValue;
    public SpriteRenderer sprite;
    public float colorStep;
    private AudioManager _audioManager;
    private IncrementalManager _incrementalManager;
    public TextMeshPro healthTMP;
    public BrickType brickType;

    public void StartBrick(Level level, LevelManager levelManager)
    {
        if (brickType == BrickType.Indestructible) return;

        _startHealth = Mathf.RoundToInt(Random.Range(startHealthRange.x, startHealthRange.y));
        var bonusHealt = levelManager.currentLevelNo / 5;
        _startHealth += bonusHealt;

        _level = level;
        _currentHealth = _startHealth;
        _incrementalManager = levelManager.gameDirector.incrementalManager;
        var greenandblueValue = 1 - _currentHealth * colorStep;
        _audioManager = levelManager.gameDirector.audioManager;
        healthTMP.text = _currentHealth.ToString();
    }

    public void GetHit()
    {
        var totalDamage = 1 + _incrementalManager.GetDamageUpgradeCount();
        _currentHealth -= totalDamage;
        var greenandblueValue = 1 - _currentHealth * colorStep;

        PlayVisualFX(greenandblueValue);
        if (brickType == BrickType.Tough) _level.ToughBrickGotHit(transform.position);
        if (_currentHealth <= 0) DestroyBrick();
        else healthTMP.text = _currentHealth.ToString();
    }

    private void PlayVisualFX(float greenandblueValue)
    {
        sprite.transform.DOKill();
        sprite.transform.localScale = Vector3.one * .45f;
        sprite.transform.localPosition = Vector3.zero;
        sprite.transform.DOScale(.55f, .1f).SetLoops(2, LoopType.Yoyo);
        healthTMP.DOKill();
        healthTMP.color = Color.white;
        healthTMP.DOColor(Color.red, .1f).SetLoops(2, LoopType.Yoyo);
        //sprite.transform.DOPunchPosition(Vector3.one * .1f, .1f, 100);
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

public enum BrickType
{
    Basic,
    Indestructible,
    Tough
}
