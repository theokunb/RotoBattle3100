using UnityEngine;
using UnityEngine.Localization.Components;

[RequireComponent(typeof(LocalizeStringEvent))]
public class DisplayLevel : MonoBehaviour
{
    [SerializeField] private Player _player;

    private LocalizeStringEvent _text;

    public int Level => _player.Level;

    private void Awake()
    {
        _text = GetComponent<LocalizeStringEvent>();
    }

    private void OnEnable()
    {
        _player.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _player.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged()
    {
        _text.RefreshString();
    }
}
