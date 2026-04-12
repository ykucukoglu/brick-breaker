using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI levelTMP;
    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(int levelNo)
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .1f);
        levelTMP.text = $"LEVEL {levelNo}";
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, .1f).OnComplete(() => gameObject.SetActive(false));
    }
}
