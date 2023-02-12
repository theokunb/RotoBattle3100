using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Bag : MonoBehaviour
{
    private List<DetailDropped> _detailsDropped;
    private List<DroppedCurrency> _currenciesDropped;

    private void Awake()
    {
        _detailsDropped = new List<DetailDropped>();
        _currenciesDropped = new List<DroppedCurrency>();
    }

    public void Put(DetailDropped detailDropped)
    {
        _detailsDropped.Add(detailDropped);
    }

    public void Put(DroppedCurrency currencyDropped)
    {
        _currenciesDropped.Add(currencyDropped);
    }

    public IEnumerable<DetailDropped> GetDetails() => _detailsDropped;

    public IEnumerable<DroppedCurrency> GetCurrencies() => _currenciesDropped;
}
