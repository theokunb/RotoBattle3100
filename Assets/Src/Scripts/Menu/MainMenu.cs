using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Subpanel _rootPanel;
    [SerializeField] private Button _playButton;
    [SerializeField] private Map _map;
    [SerializeField] private Player _player;
    [SerializeField] private LoadingPanel _loadingPanel;

    private int _levelId;

    private void Start()
    {
        _levelId = _player.Progress.CompletedLevels;
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayClicked);
        _map.CurrentLevelChanged += OnCurrentLevelChanged;
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayClicked);
        _map.CurrentLevelChanged -= OnCurrentLevelChanged;
    }

    public void OpenMenu(Subpanel subpanel)
    {
        subpanel.Push(subpanel, _rootPanel);
    }

    private void OnPlayClicked()
    {
        _loadingPanel.gameObject.SetActive(true);
        _loadingPanel.Open(GameScene.LoadAsync(_levelId));
    }

    private void OnCurrentLevelChanged(Level level)
    {
        _levelId = level.Id;
    }
}
