using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DetailView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    public event Action<DetailView> ButtonClicked;

    public IEnumerable<Currency> FullPrice { get; private set; }
    public Detail Detail { get; private set; }
    public Image Image => _image;
    public DetailShop DetailShop { get; private set; }


    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void Render(DetailShop detailShop)
    {
        _image.sprite = detailShop.Icon;
        DetailShop = detailShop;
        Detail = detailShop.GetComponent<Detail>();
        FullPrice = detailShop.GetCurrencies();
    }

    private void OnButtonClicked()
    {
        ButtonClicked?.Invoke(this);
    }
}
