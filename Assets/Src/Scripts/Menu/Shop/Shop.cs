using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemsCollectionView _template;
    [SerializeField] private GameObject _container;
    [SerializeField] private ItemsPull _items;
    [SerializeField] private BuyWindow _buyWindow;
    [SerializeField] private GameObject _emptyShopTemplate;
    [SerializeField] private ScrollRect _scrollRect;

    private Type[] _detailTypes = { typeof(Head), typeof(Body), typeof(Leg), typeof(Weapon) };
    private List<ItemsCollectionView> _collections;
    private List<Detail> _details => _items.Details
            .Select(element => element.GetComponent<DetailStatus>())
            .Where(element => element.IsAvailable == false && element.CanBuyInShop == true)
            .Select(element => element.GetComponent<Detail>())
            .ToList();

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
        if(_details.Count == 0)
        {
            Instantiate(_emptyShopTemplate, _container.transform);
            return;
        }

        foreach(var type in _detailTypes)
        {
            var selectedItems = _details
                .TakeByType(type)
                .Select(element => element.GetComponent<DetailShop>())
                .ToList();

            if(selectedItems.Count() > 0)
            {
                var collection = Instantiate(_template, _container.transform);
                collection.Render(selectedItems);
                collection.SetParent(_scrollRect);
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

        var collection = item.GetComponentInParent<ItemsCollectionView>();
        collection.Remove(item);
        Destroy(item.gameObject);
        ClearEmptyCollections();
    }

    private void ClearEmptyCollections()
    {
        foreach(var collection in _collections)
        {
            if(collection.Count == 0)
            {
                Destroy(collection.gameObject);
            }
        }

        if (_details.Count == 0)
        {
            Instantiate(_emptyShopTemplate, _container.transform);
            return;
        }
    }
}

public static class DetailsShopExtension
{
    public static IEnumerable<Detail> TakeByType(this IEnumerable<Detail> items, Type type)
    {
        return items.Where(element => element.GetType() == type);
    }
}
