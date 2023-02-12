using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ChangabeButton : MonoBehaviour
{
    [SerializeField] private Sprite _active;
    [SerializeField] private Sprite _inactive;

    private Image _image;
    private Button _button;

    public event Action<ChangabeButton> Performed;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
        
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Performed?.Invoke(this);
    }

    public void SetInactive()
    {
        if(_image == null)
        {
            return;
        }

        _image.sprite = _inactive;
    }

    public void SetActive()
    {
        if (_image == null)
        {
            return;
        }

        _image.sprite = _active;
    }
}
