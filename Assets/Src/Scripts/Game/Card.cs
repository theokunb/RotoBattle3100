using UnityEngine;
using UnityEngine.Localization;

public class Card : MonoBehaviour
{
    [SerializeField] private LocalizedString _title;
    [SerializeField] private LocalizedString _description;
    [SerializeField] private Sprite _mainImage;
    [SerializeField] private Sprite _backImage;
    [SerializeField] private Upgrades _upgrade;
    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private Animator _animator;

    public Sprite MainImage => _mainImage;
    public Sprite BackImage => _backImage;
    public string Title => _title.GetLocalizedString();
    public string Description => _description.GetLocalizedString();
    public RenderTexture Texture => _renderTexture;
    public Upgrades Upgrade => _upgrade;

    public void Open()
    {
        _title.RefreshString();
        _description.RefreshString();
        _animator?.SetTrigger(CardAnimationController.Params.Open);
    }

    public void Close()
    {
        _animator?.SetTrigger(CardAnimationController.Params.Close);
    }
}
