using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class ItemsCollectionView : MonoBehaviour
{
    [SerializeField] private DetailView _template;
    [SerializeField] private GameObject _container;
    [SerializeField] private TMP_Text _labelText;

    private List<DetailView> _items;
    private LocalizedString _label;
    
    public event Action<DetailView> ItemSelected;

    public int Count => _items.Count;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        foreach(var item in _items)
        {
            item.ButtonClicked -= DetailBuyButtonClicked;
        }

        if(_label != null)
        {
            _label.StringChanged -= OnLabelStringChanged;
        }
    }

    private void Awake()
    {
        _items= new List<DetailView>();
    }

    public void Render(IEnumerable<DetailShop> details)
    {
        var firstDetail = details.FirstOrDefault();
        
        if(firstDetail != null)
        {
            _label = firstDetail.GetComponent<Detail>().Label;
        }

        foreach(var detail in details)
        {
            var createdDetail = Instantiate(_template, _container.transform);
            createdDetail.Render(detail);
            _items.Add(createdDetail);
        }

        Subscribe();
    }

    private void DetailBuyButtonClicked(DetailView obj)
    {
        ItemSelected?.Invoke(obj);
    }

    private void Subscribe()
    {
        foreach(var item in _items)
        {
            item.ButtonClicked += DetailBuyButtonClicked;
        }

        if(_label != null)
        {
            _label.StringChanged += OnLabelStringChanged;
        }
    }

    private void OnLabelStringChanged(string value)
    {
        _labelText.text = value;
    }

    public void Remove(DetailView detailView)
    {
        _items.Remove(detailView);
    }
}
