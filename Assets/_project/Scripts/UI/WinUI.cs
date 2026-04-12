using DG.Tweening;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    public UIManager uiManager;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(float delay)
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .1f).SetDelay(delay);
    }
    public void Hide()
    {
        _canvasGroup.DOFade(0, .1f).OnComplete(() => gameObject.SetActive(false));
    }

    public void LoadNextLevelButtonPressed()
    {
        uiManager.LoadNextLevelButtonPressed();
        Hide();
    }
}
