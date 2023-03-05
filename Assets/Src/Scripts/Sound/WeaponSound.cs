using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponSound : MonoBehaviour
{
    private AudioSource _audioSource;
    private SoundController _sounds;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _sounds = FindObjectOfType<SoundController>();
    }

    public void Play()
    {
        if (_sounds != null)
        {
            var freeAudioSource = _sounds.GetAudioSource();

            freeAudioSource?.PlayOneShot(_audioSource.clip);
        }
    }
}
