using UnityEngine;
using UnityEngine.UI;

public class BackgroundAudioPlayer : MonoBehaviour
{
    [SerializeField] private SoundController _sounds;
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioClip _backgroundTheme;

    private AudioSource _backgroundPlayer;

    protected SoundController Sounds => _sounds;

    protected virtual void Start()
    {
        float volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.Volume, 0.1f);

        if (_slider != null)
        {
            _slider.value = volume;
        }
        else
        {
            _sounds.SetVolume(volume);
        }

        PlayBackgroundTheme();
    }

    public void PlayBackgroundTheme()
    {
        _backgroundPlayer = _sounds.GetAudioSource();
        
        if (_backgroundPlayer != null)
        {
            _backgroundPlayer.PlayOneShot(_backgroundTheme);
            _backgroundPlayer.loop = true;
        }
    }

    public void StopBackgroundTheme()
    {
        _backgroundPlayer.loop = false;
        _backgroundPlayer.Stop();
    }

    public void Pause()
    {
        _backgroundPlayer.mute = true;
    }

    public void Resume()
    {
        _backgroundPlayer.mute = false;
    }
}
