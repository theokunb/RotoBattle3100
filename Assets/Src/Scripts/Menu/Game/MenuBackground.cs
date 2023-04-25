using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuBackground : MonoBehaviour
{
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _translationTime;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Menu[] _menus;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void OnepMenu(Menu menu)
    {
        menu.gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, _fadeTime).SetUpdate(true);

        menu.transform.localPosition = new Vector3(0,-Screen.height, 0);
        menu.GetComponent<RectTransform>().DOAnchorPosY(0, _translationTime).SetUpdate(true).OnComplete(() =>
        {
            menu.Activated();
        });
    }

    public void CloseMenu(Menu menu)
    {
        _canvasGroup.DOFade(0, _fadeTime).SetUpdate(true);

        menu.GetComponent<RectTransform>().DOAnchorPosY(-Screen.height, _translationTime).SetUpdate(true).OnComplete(() => 
        {
            menu.gameObject.SetActive(false);
            _canvasGroup.gameObject.SetActive(false);
        });
    }
}
