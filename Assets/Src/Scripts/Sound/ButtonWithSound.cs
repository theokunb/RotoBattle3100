using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class ButtonWithSound : MonoBehaviour
{
    [SerializeField] private SoundController _sounds;

    private Button _button;
    private AudioSource _audioSource;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();

        if(_sounds == null)
        {
            _sounds = FindObjectOfType<SoundController>();
        }
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnPlay);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnPlay);
    }

    private void OnPlay()
    {
        var freeAudioSource = _sounds.GetAudioSource();

        if (freeAudioSource != null)
        {
            freeAudioSource.PlayOneShot(_audioSource.clip);
        }
    }
}
