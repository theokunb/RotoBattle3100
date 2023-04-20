using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class InventoryObserver : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ItemsPull _itemPull;
    [SerializeField] private GameObject _itemsContainer;
    [SerializeField] private DetailView _template;
    [SerializeField] private GameObject _informationWindow;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;

    private void OnDisable()
    {
        _player.Save();
    }

    public void ShowItems(DetailType detailType)
    {
        ClearView();
        var details = GetAvailableDetails(detailType);

        foreach (var detailShop in details)
        {
            var detailView = Instantiate(_template, _itemsContainer.transform);
            detailView.Render(detailShop);
            detailView.ButtonClicked += OnItemClicked;
            detailView.ButtonHighlited += DetailView_ButtonHighlited;
        }
    }

    public void ClearView()
    {
        foreach (Transform child in _itemsContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerable<DetailShop> GetAvailableDetails(DetailType detailType)
    {
        switch(detailType)
        {
            case DetailType.Head:
                return GetAvailableDetails(typeof(Head));
            case DetailType.Body:
                return GetAvailableDetails(typeof(Body));
            case DetailType.Leg:
                return GetAvailableDetails(typeof(Leg));
            case DetailType.Weapons:
                return GetAvailableDetails(typeof(Weapon));
            default:
                return null;
        }
    }

    private IEnumerable<DetailShop> GetAvailableDetails(Type type)
    {
        return _itemPull.Details.Where(detail => detail.IsAvailable == true && detail.GetType() == type).Select(detail => detail.GetComponent<DetailShop>()).ToList();
    }

    private void OnItemClicked(DetailView detailView)
    {
        var detail = detailView.DetailShop.GetComponent<Detail>();
        _player.SetDetail(detail);
        _player?.CorrectDetails();

        DisplayInfo(detail);
    }

    private void DisplayInfo(Detail detail)
    {
        StringBuilder descriptionBuilder = new StringBuilder();
        descriptionBuilder.Append(detail.Description +"\n");
        descriptionBuilder.Append(detail.GetStats());

        _title.text = detail.Title;
        _description.text = descriptionBuilder.ToString();
    }

    private void DetailView_ButtonHighlited(DetailView detailView)
    {
        var detail = detailView.DetailShop.GetComponent<Detail>();
        DisplayInfo(detail);
    }
}
