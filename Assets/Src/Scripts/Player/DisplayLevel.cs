using UnityEngine;
using UnityEngine.Localization.Components;

[RequireComponent(typeof(LocalizeStringEvent))]
public class DisplayLevel : MonoBehaviour
{
    [SerializeField] private Player _player;

    private LocalizeStringEvent _text;

    public int Level => _player.Experience.Level;

    private void OnEnable()
    {
        _player.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _player.LevelChanged -= OnLevelChanged;
    }

    private void Start()
    {
        _text = GetComponent<LocalizeStringEvent>();
        OnLevelChanged();
    }

    private void OnLevelChanged()
    {
        _text.RefreshString();
    }
}
