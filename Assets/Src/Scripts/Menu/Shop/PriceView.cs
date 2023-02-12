using System.Collections.Generic;
using UnityEngine;

public class PriceView : MonoBehaviour
{
    [SerializeField] private Sprite _metal;
    [SerializeField] private Sprite _energy;
    [SerializeField] private Sprite _fuel;
    [SerializeField] private DisplayCurrency _template;
    [SerializeField] private Transform _container;

    public void Display(IEnumerable<Currency> currencies)
    {
        ClearChilds();

        foreach (Currency currency in currencies)
        {
            var displayCurrency = Instantiate(_template, _container);
            displayCurrency.Render(GetSprite(currency), currency);
        }
    }

    private void ClearChilds()
    {
        foreach(Transform child in _container)
        {
            Destroy(child.gameObject);
        }
    }

    private Sprite GetSprite(Currency currency)
    {
        if(currency is Metal)
        {
            return _metal;
        }
        else if(currency is Energy)
        {
            return _energy;
        }
        else if(currency is Fuel)
        {
            return _fuel;
        }
        else
        {
            return null;
        }
    }
}
