using UnityEngine;
using UnityEngine.UI;

public class DisplayExperience : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _progressBar;

    private void OnEnable()
    {
        _player.ExperienceChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.ExperienceChanged -= OnValueChanged;
    }

    private void Start()
    {
        OnValueChanged();
    }

    private void OnValueChanged()
    {
        if(_progressBar != null)
        {
            _progressBar.fillAmount = (float)_player.Experience.CurrentValue / _player.Experience.MaxValue;
        }
    }
}
