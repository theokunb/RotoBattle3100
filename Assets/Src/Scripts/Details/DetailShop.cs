using System.Collections.Generic;
using UnityEngine;

public class DetailShop : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Price[] _prices;

    public Sprite Icon => _icon;

    public IEnumerable<Currency> GetCurrencies()
    {
        foreach(var price in _prices)
        {
            yield return price.ToCurrency();
        }
    }
}
