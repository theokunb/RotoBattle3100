using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu
{
    [SerializeField] private MenuBackground _menuBackground;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private LoadingPanel _loadingPanel;

    protected override void OnEnable()
    {
        base.OnEnable();

        _resumeButton.onClick.AddListener(OnResumeClicked);
        _exitButton.onClick.AddListener(OnExitClicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _resumeButton.onClick.RemoveListener(OnResumeClicked);
        _exitButton.onClick.RemoveListener(OnExitClicked);
    }

    private void OnResumeClicked()
    {
        _menuBackground.CloseMenu(this);
    }

    private void OnExitClicked()
    {
        _loadingPanel.gameObject.SetActive(true);
        _loadingPanel.Open(MenuScene.LoadAsync());
    }

    public override void Activated()
    {

    }
}
