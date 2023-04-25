using UnityEngine;
using UnityEngine.UI;

public class SoundSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite _active;
    [SerializeField] private Sprite _inactive;
    [SerializeField] private Image _targetImage;
    [SerializeField] private SoundContainer _soundController;
    [SerializeField] private float _defaultVolume = 0.1f;

    private float _previousVolume;
    private bool _isMuted;

    private void OnEnable()
    {
        _isMuted = _soundController.GetVolume() == 0;
        UpdateImage();
    }

    private void UpdateImage()
    {
        _targetImage.sprite = _isMuted ? _inactive : _active;
    }

    private void Enable()
    {
        _isMuted = false;

        if(_previousVolume == 0)
        {
            _previousVolume = _defaultVolume;
        }

        _soundController.SetVolume(_previousVolume);
        UpdateImage();
    }

    private void Disable()
    {
        _isMuted = true;
        _previousVolume = _soundController.GetVolume();
        _soundController.SetVolume(0);
        UpdateImage();
    }

    public void OnSoundSwitch()
    {
        if (_isMuted)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }
}
