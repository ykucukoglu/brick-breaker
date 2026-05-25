using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncrementalUI : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public IncrementalManager incrementalManager;
    public CoinManager coinManager;
    public Button damageUpgradeButton;
    public TextMeshProUGUI damageCostTMP;
    public TextMeshProUGUI curDamageLevelTMP;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(float delay)
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .1f).SetDelay(delay);
        RefreshButtons();
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, .1f).OnComplete(() => gameObject.SetActive(false));
    }

    public void DamageUpgradeButtonPressed()
    {
        incrementalManager.DamageUpgradeButtonPressed();
        RefreshButtons();
    }

    public void RefreshButtons()
    {
        var cost = incrementalManager.GetDamageUpgradeCost();
        var coinCount = coinManager.cointCount;
        if (coinCount >= cost)
            damageUpgradeButton.interactable = true;
        else
            damageUpgradeButton.interactable = false;

        damageCostTMP.text = cost.ToString();
        curDamageLevelTMP.text = $"DAMAGE LEVEL {incrementalManager.GetDamageUpgradeCount().ToString()}";
    }
}
