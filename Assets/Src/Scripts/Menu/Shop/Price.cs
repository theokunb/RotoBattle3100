using System;
using UnityEngine;

[Serializable]
public class Price
{
    [SerializeField] private CurrencyType _currencyType;
    [SerializeField] private int _count;

    public Price(CurrencyType currencyType, int count)
    {
        _currencyType = currencyType;
        _count = count;
    }

    public CurrencyType CurrencyType => _currencyType;
    public int Count => _count;
}

public static class PriceExtension
{
    public static Currency ToCurrency(this Price price)
    {
        switch (price.CurrencyType)
        {
            case CurrencyType.Metal:
                return new Metal(price.Count);
            case CurrencyType.Energy:
                return new Energy(price.Count);
            case CurrencyType.Fuel:
                return new Fuel(price.Count);
            default:
                return null;
        }
    }
}

public enum CurrencyType
{
    Fuel,
    Metal,
    Energy
}