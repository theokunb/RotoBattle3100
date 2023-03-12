using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu
{
    [SerializeField] private MenuBackground _menuBackground;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private LoadingPanel _loadingPanel;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeClicked);
        _exitButton.onClick.AddListener(OnExitClicked);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeClicked);
        _exitButton.onClick.RemoveListener(OnExitClicked);
    }

    private void OnResumeClicked()
    {
        _menuBackground.CloseMenu(this);
    }

    private void OnExitClicked()
    {
        Time.timeScale = 1;

        _loadingPanel.gameObject.SetActive(true);
        _loadingPanel.Open(MenuScene.LoadAsync());
    }

    public override void Activated()
    {

    }
}
