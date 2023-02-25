using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    private Level _level;
    private LevelStatus _levelStatus;

    public event Action<Level> LevelSelected;

    public string LevelLabel => _level?.Title;
    public string LevelStatusLabel => _levelStatus?.TodayCompletedTimes.ToString();

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void Render(Level level, LevelStatus status)
    {
        _level = level;
        _levelStatus = status;
        _image.sprite = level.Icon;
        _button.interactable = status.IsCompleted;

        _text.GetComponent<LocalizeStringEvent>().RefreshString();
    }

    private void OnButtonClicked()
    {
        LevelSelected?.Invoke(_level);
    }
}
