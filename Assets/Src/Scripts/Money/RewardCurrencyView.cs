using System.Collections.Generic;
using UnityEngine;

public class RewardCurrencyView : RewardView
{
    [SerializeField] private Sprite _metal;
    [SerializeField] private Sprite _fuel;
    [SerializeField] private Sprite _energy;
    [SerializeField] private DisplayCurrency _template;
    [SerializeField] private GameObject _levelRewards;
    [SerializeField] private GameObject _collectedRewards;

    public void RenderLevelReward(IEnumerable<Currency> currencies)
    {
        foreach (var currency in currencies)
        {
            var displayCurrency = Instantiate(_template, _levelRewards.transform);
            displayCurrency.Render(GetSprite(currency), currency);
        }
    }

    public void RenderCollectedReward(IEnumerable<Currency> currencies)
    {
        foreach (var currency in currencies)
        {
            if(currency.Count == 0)
            {
                continue;
            }

            var displayCurrency = Instantiate(_template, _collectedRewards.transform);
            displayCurrency.Render(GetSprite(currency), currency);
        }
    }

    private Sprite GetSprite(Currency currency)
    {
        if (currency is Metal)
        {
            return _metal;
        }
        else if (currency is Fuel)
        {
            return _fuel;
        }
        return _energy;
    }
}
