using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Localization.Components;

public class BuyWindow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private PriceView _priceView;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _stats;

    private DetailView _item;

    public event Action<DetailView, bool> DialogResult;

    public string ItemTitle => _item?.Detail?.Title;
    public string ItemDescription => _item?.Detail?.Description;
    public string ItemStats => _item?.Detail?.GetStats();

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnCloseClicked);
        _buyButton.onClick.AddListener(OnBuyClicked);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseClicked);
        _buyButton.onClick.RemoveListener(OnBuyClicked);
    }

    public void Render(DetailView item)
    {
        _item = item;
        _image.sprite = item.Image.sprite;
        _priceView.Display(item.FullPrice);
        _buyButton.interactable = _playerWallet.CanBuy(item.FullPrice);

        _title.GetComponent<LocalizeStringEvent>().RefreshString();
        _description.GetComponent<LocalizeStringEvent>().RefreshString();
        _stats.GetComponent<LocalizeStringEvent>().RefreshString();
    }

    private void OnBuyClicked()
    {
        _playerWallet.Buy(_item);
        DialogResult?.Invoke(_item, true);
        gameObject.SetActive(false);
    }

    private void OnCloseClicked()
    {
        DialogResult?.Invoke(_item, false);
        gameObject.SetActive(false);
    }
}
