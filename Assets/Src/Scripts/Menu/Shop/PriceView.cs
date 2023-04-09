using System.Collections.Generic;
using UnityEngine;

public class PriceView : MonoBehaviour
{
    [SerializeField] private Sprite _metal;
    [SerializeField] private Sprite _energy;
    [SerializeField] private Sprite _fuel;
    [SerializeField] private DisplayCurrency _template;
    [SerializeField] private Transform _container;

    private ICurrencyRenderer _currencyRenderer;

    private void Awake()
    {
        _currencyRenderer = new CurrencyRenderer(_metal, _energy, _fuel);
    }

    public void Display(IEnumerable<Currency> currencies)
    {
        ClearChilds();

        foreach (Currency currency in currencies)
        {
            var displayCurrency = Instantiate(_template, _container);
            currency.Accept(_currencyRenderer, displayCurrency);
        }
    }

    private void ClearChilds()
    {
        foreach(Transform child in _container)
        {
            Destroy(child.gameObject);
        }
    }
}
