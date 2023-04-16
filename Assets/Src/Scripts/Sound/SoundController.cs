using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource[] _audioSources;

    private void Awake()
    {
        _audioSources = GetComponentsInChildren<AudioSource>();
    }

    public AudioSource GetAudioSource()
    {
        foreach (var audioSource in _audioSources)
        {
            if (audioSource.isPlaying == false)
            {
                return audioSource;
            }
        }

        return null;
    }

    public void SetVolume(float value)
    {
        foreach(var audioSource in _audioSources)
        {
            audioSource.volume = value;
            PlayerPrefs.SetFloat(PlayerPrefsKeys.Volume, value);
        }
    }

    public float GetVolume()
    {
        float value = PlayerPrefs.GetFloat(PlayerPrefsKeys.Volume);
        return value;
    }
}
