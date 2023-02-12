using UnityEngine;
using DG.Tweening;

public class PopUpMenuAnimation : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeTime;

    private void OnEnable()
    {
        _canvasGroup.DOFade(0.8f, _fadeTime).OnComplete(()=>
        {
            _canvasGroup.blocksRaycasts = true;
        });
    }

    private void OnDisable()
    {
        _canvasGroup.DOFade(0f, _fadeTime).OnComplete(() =>
        {
            _canvasGroup.blocksRaycasts = false;
        });
    }
}
