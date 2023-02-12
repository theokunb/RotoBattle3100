using IJunior.TypedScenes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Subpanel _rootPanel;
    [SerializeField] private Button _playButton;
    [SerializeField] private Map _map;
    [SerializeField] private PlayerLoader _loader;

    private int _levelId;

    private void Awake()
    {
        _levelId = _loader.PlayerProgress.CompletedLevels;
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
        GameScene.Load(_levelId);
    }

    private void OnCurrentLevelChanged(Level level)
    {
        _levelId = level.Id;
    }
}
