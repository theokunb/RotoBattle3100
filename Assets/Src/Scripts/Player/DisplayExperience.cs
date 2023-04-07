using UnityEngine;
using UnityEngine.UI;

public class DisplayExperience : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _progressBar;

    private void Start()
    {
        OnValueChanged();
    }

    private void OnValueChanged()
    {
        if(_progressBar != null)
        {
            _progressBar.fillAmount = (float)_player.CurrentValue / _player.MaxValue;
        }
    }
}
