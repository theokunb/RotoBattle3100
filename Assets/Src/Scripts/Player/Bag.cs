using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Bag : MonoBehaviour
{
    private List<DetailDropped> _detailsDropped;
    private Wallet _wallet;

    private void Awake()
    {
        _detailsDropped = new List<DetailDropped>();
        _wallet = new Wallet();
    }

    public void Put(DetailDropped detailDropped)
    {
        _detailsDropped.Add(detailDropped);
    }

    public void Put(DroppedCurrency currencyDropped)
    {
        _wallet.Increase(currencyDropped.Currency);
    }

    public IEnumerable<DetailDropped> GetDetails() => _detailsDropped;

    public IEnumerable<Currency> GetCurrencies() => _wallet.GetCurrencies();
}
