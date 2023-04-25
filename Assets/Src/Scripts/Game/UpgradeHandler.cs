using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField] private FadableImage _upgradePanel;
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
        _upgradePanel.gameObject.SetActive(true);
        MinimizeCards();

        _upgradePanel.FadeIn(() =>
        {
            OpenCards();
        });
    }

    private void HidePanel()
    {
        _upgradePanel.FadeOut(() =>
        {
            CloseCards();
            _upgradePanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
        });
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
