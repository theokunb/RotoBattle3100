using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private Player _player;
    [SerializeField] private CardHandler[] _cardHandlers;

    private void OnEnable()
    {
        _player.LevelChanged += OnLevelUp;
    }

    private void OnDisable()
    {
        _player.LevelChanged -= OnLevelUp;
    }

    private void OnLevelUp()
    {
        ShowPanel();
    }

    private void ShowPanel()
    {
        Time.timeScale = 0f;
        _upgradePanel.SetActive(true);
        MinimizeCards();

        var image = _upgradePanel.GetComponent<Image>();

        if (image != null)
        {
            image.color = new Color(0, 0, 0, 0);
            image.DOFade(0.9f, 0.5f).SetUpdate(true).OnComplete(() =>
            {
                OpenCards();
            });
        }
    }

    private void HidePanel()
    {
        var image = _upgradePanel.GetComponent<Image>();

        if (image != null)
        {
            image.DOFade(0, 0.2f).SetUpdate(true).OnComplete(() =>
            {
                CloseCards();
                _upgradePanel.SetActive(false);
                Time.timeScale = 1f;
            });
        }
    }

    private void MinimizeCards()
    {
        foreach (var cardHandler in _cardHandlers)
        {
            cardHandler.Minimize();
        }
    }

    private void OpenCards()
    {
        foreach (var cardHandler in _cardHandlers)
        {
            cardHandler.OnClick += OnClicked;
            cardHandler.OpenCard();
        }
    }

    private void CloseCards()
    {
        foreach (var cardHandler in _cardHandlers)
        {
            cardHandler.CloseCard();
        }
    }

    private void OnClicked(Upgrades upgrade)
    {
        _player.AddUpgrade(upgrade);

        HidePanel();
    }
}
