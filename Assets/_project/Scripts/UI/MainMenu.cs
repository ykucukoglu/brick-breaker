using DG.Tweening;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public UIManager uiManager;
    public CanvasGroup canvasGroup;
    public void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1, .1f);

    }

    public void Hide()
    {
        canvasGroup.DOFade(0, .1f).OnComplete(() => gameObject.SetActive(false));
    }

    public void PlayGameButtonPressed()
    {
        uiManager.PlayGameButtonPressed();
        Hide();
    }
}
