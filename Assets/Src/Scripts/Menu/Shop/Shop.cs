using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemsCollectionView _template;
    [SerializeField] private GameObject _container;
    [SerializeField] private ItemsPull _items;
    [SerializeField] private BuyWindow _buyWindow;

    private Type[] _detailTypes = { typeof(Head), typeof(Body), typeof(Leg), typeof(Weapon) };
    private List<ItemsCollectionView> _collections;


    private void Awake()
    {
        _collections = new List<ItemsCollectionView>();
    }
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        foreach(var collection in _collections)
        {
            collection.ItemSelected -= OnItemSelected;
        }
    }

    private void Start()
    {
        var items = _items.Details
            .Select(element => element.GetComponent<DetailStatus>())
            .Where(element => element.IsAvailable == false && element.CanBuyInShop == true)
            .Select(element => element.GetComponent<Detail>())
            .ToList();

        foreach(var type in _detailTypes)
        {
            var selectedItems = items
                .TakeByType(type)
                .Select(element => element.GetComponent<DetailShop>())
                .ToList();

            if(selectedItems.Count() > 0)
            {
                var collection = Instantiate(_template, _container.transform);
                collection.Render(selectedItems);
                _collections.Add(collection);
            }
        }

        Subscribe();
    }

    private void Subscribe()
    {
        foreach(var collection in _collections)
        {
            collection.ItemSelected += OnItemSelected;
        }
    }

    private void OnItemSelected(DetailView itemShopView)
    {
        _buyWindow.gameObject.SetActive(true);
        _buyWindow.Render(itemShopView);
        _buyWindow.DialogResult += OnDialogResult;
    }

    private void OnDialogResult(DetailView item,bool result)
    {
        _buyWindow.DialogResult -= OnDialogResult;

        if (result == false)
        {
            return;
        }
        
        Destroy(item.gameObject);
    }
}

public static class DetailsShopExtension
{
    public static IEnumerable<Detail> TakeByType(this IEnumerable<Detail> items, Type type)
    {
        return items.Where(element => element.GetType() == type);
    }
}
