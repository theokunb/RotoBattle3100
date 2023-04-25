using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(RawImage))]
public class CardHandler : MonoBehaviour
{
    [SerializeField] private Card _card;
    [SerializeField] private DotweenAnimation _scaleAnimation;

    private Button _button;
    private RawImage _rawImage;

    public event Action<Upgrades> OnClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _rawImage = GetComponent<RawImage>();

        _rawImage.texture = _card.Texture;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Clicked);
        _button.interactable = true;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Clicked);
    }

    private void Clicked()
    {
        OnClick?.Invoke(_card.Upgrade);
        _button.interactable = false;
    }

    public void OpenCard()
    {
        _scaleAnimation.Animate(() =>
        {
            _card?.Open();
        });
    }

    public void CloseCard()
    {
        _card?.Close();
    }

    public void Minimize()
    {
        transform.localScale = Vector3.zero;
    }
}
