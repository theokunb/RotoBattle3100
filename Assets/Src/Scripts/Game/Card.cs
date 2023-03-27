using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _mainImage;
    [SerializeField] private Sprite _backImage;
    [SerializeField] private Upgrades _upgrade;
    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private Animator _animator;

    public Sprite MainImage => _mainImage;
    public Sprite BackImage => _backImage;
    public string Title => _title;
    public string Description => _description;
    public RenderTexture Texture => _renderTexture;
    public Upgrades Upgrade => _upgrade;

    public void Open()
    {
        _animator?.SetTrigger(CardAnimationController.Params.Open);
    }

    public void Close()
    {
        _animator?.SetTrigger(CardAnimationController.Params.Close);
    }
}
