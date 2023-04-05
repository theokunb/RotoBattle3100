using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] private LevelsContainer _levelContainer;
    [SerializeField] private Player _player;
    [SerializeField] private LevelView _template;
    [SerializeField] private GameObject _container;
    [SerializeField] private Button _backButton;

    private List<LevelView> _levels = new List<LevelView>();

    public event Action<Level> CurrentLevelChanged;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    private void Start()
    {
        foreach (var level in _levelContainer.Levels)
        {
            var createdLevel = Instantiate(_template, _container.transform);
            createdLevel.Render(level, _player.Progress.GetStatus(level.Id));

            _levels.Add(createdLevel);
        }

        Subscribe();
    }

    private void Subscribe()
    {
        foreach (var element in _levels)
        {
            element.LevelSelected += OnLevelSelected;
        }
    }

    private void UnSubscribe()
    {
        foreach (var element in _levels)
        {
            element.LevelSelected -= OnLevelSelected;
        }
    }

    private void OnLevelSelected(Level level)
    {
        CurrentLevelChanged?.Invoke(level);
        _backButton.onClick.Invoke();
    }
}
