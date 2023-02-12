using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "currency pull", order = 51)]
public class CurrencyPull : ScriptableObject
{
    [SerializeField] private DroppedCurrency[] _dropCurrency;

    public IEnumerable<DroppedCurrency> DropCurrency => _dropCurrency;

    public DroppedCurrency GetRandomCurrency()
    {
        if (_dropCurrency.Length == 0)
        {
            return null;
        }

        int index = Random.Range(0, _dropCurrency.Length);
        return _dropCurrency[index];
    }
}
