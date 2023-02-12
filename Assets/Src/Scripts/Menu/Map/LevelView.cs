using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    private Button _button;

    public event Action<Level> LevelSelected;

    public Level Level { get;private set; }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

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
        Level = level;
        _image.sprite = level.Icon;
        _text.text = $"{level.Title}\nпройдено {status.TodayCompletedTimes} раз";
        _button.interactable = status.IsCompleted;
    }

    private void OnButtonClicked()
    {
        LevelSelected?.Invoke(Level);
    }
}
