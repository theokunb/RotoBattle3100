using UnityEngine;
using UnityEngine.UI;

public class DisplayExperience : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _progressBar;

    public int Level => _player.Level;

    private void Start()
    {
        OnValueChanged();
    }

    private void OnEnable()
    {
        _player.ExperiencChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.ExperiencChanged -= OnValueChanged;
    }

    private void OnValueChanged()
    {
        if(_progressBar != null)
        {
            _progressBar.fillAmount = (float)_player.CurrentValue / _player.MaxValue;
        }
    }
}