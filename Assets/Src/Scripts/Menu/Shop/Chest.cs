using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] private ItemsPull _items;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _disabledColor;
    [SerializeField] private Color _enabledColor;
    [SerializeField, Range(0, 23)] private int _hoursColdown;
    [SerializeField, Range(0, 59)] private int _minutesColdown;

    private Button _button;
    private DateTime _lastOpenedDate;
    private float _elapsedTime;
    private TimeSpan _timeDelay;

    public event Action<Detail, DateTime> Opened;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _elapsedTime = 0;
        _timeDelay = new TimeSpan(_hoursColdown, _minutesColdown, 0);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= 1)
        {
            _elapsedTime = 0;

            if (DateTime.Now - _lastOpenedDate < _timeDelay)
            {
                _button.interactable = false;
                _image.color = _disabledColor;

                var time = (_timeDelay - (DateTime.Now - _lastOpenedDate));

                _text.text = time.ToString(@"hh\:mm\:ss");
            }
            else
            {
                _button.interactable = true;
                _image.color = _enabledColor;
                _text.text = string.Empty;
            }
        }
    }

    public void SetLastOpenedDate(DateTime dateTime)
    {
        _lastOpenedDate = dateTime;
    }

    private void OnClick()
    {
        Detail detail = _items.GetRandomDetail();

        Opened?.Invoke(detail, DateTime.Now);
    }
}
