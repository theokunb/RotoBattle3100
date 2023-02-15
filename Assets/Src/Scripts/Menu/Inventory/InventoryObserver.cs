using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryObserver : MonoBehaviour
{
    private const string DefaultInfo = "Информация";

    [SerializeField] private Player _player;
    [SerializeField] private ItemsPull _itemPull;
    [SerializeField] private GameObject _itemsContainer;
    [SerializeField] private DetailView _template;
    [SerializeField] private GameObject _informationWindow;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;

    public void ShowItems(DetailType detailType)
    {
        ClearView();
        InformationWindowClear();

        var details = GetAvailableDetails(detailType);

        foreach (var detailShop in details)
        {
            var detailView = Instantiate(_template, _itemsContainer.transform);
            detailView.Render(detailShop);
            detailView.ButtonClicked += OnItemClicked;
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
        _player.SetDetail((dynamic)detail);
        _player.CorrectDetails(_player.LegPosition);
        _player.Save();

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

    private void InformationWindowClear()
    {
        _title.text = DefaultInfo;
        _description.text = string.Empty;
    }
}
