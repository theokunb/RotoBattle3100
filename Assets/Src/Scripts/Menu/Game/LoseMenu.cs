using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : Menu
{
    [SerializeField] private MenuBackground _menuBackground;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Game _game;
    [SerializeField] private LoadingPanel _loadingPanel;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(OnHomeClicked);
        _replayButton.onClick.AddListener(OnReplayClicked);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(OnHomeClicked);
        _replayButton.onClick.RemoveListener(OnReplayClicked);
    }

    private void OnHomeClicked()
    {
        _menuBackground.CloseMenu(this);

        _loadingPanel.gameObject.SetActive(true);
        _loadingPanel.Open(MenuScene.LoadAsync());
    }

    private void OnReplayClicked()
    {
        _menuBackground.CloseMenu(this);

        _loadingPanel.gameObject.SetActive(true);
        _loadingPanel.Open(GameScene.LoadAsync(_game.CurrentLevel.Id));
    }

    public override void Activated()
    {

    }
}
