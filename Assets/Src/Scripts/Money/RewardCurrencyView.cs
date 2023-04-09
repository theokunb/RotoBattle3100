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

    private ICurrencyRenderer _currencyRenderer;

    public void RenderLevelReward(IEnumerable<Currency> currencies)
    {
        _currencyRenderer = new CurrencyRenderer(_metal, _fuel, _energy);

        foreach (var currency in currencies)
        {
            var displayCurrency = Instantiate(_template, _levelRewards.transform);
            currency.Accept(_currencyRenderer, displayCurrency);
        }
    }

    public void RenderCollectedReward(IEnumerable<Currency> currencies)
    {
        _currencyRenderer = new CurrencyRenderer(_metal, _fuel, _energy);

        foreach (var currency in currencies)
        {
            if(currency.Count == 0)
            {
                continue;
            }

            var displayCurrency = Instantiate(_template, _collectedRewards.transform);
            currency.Accept(_currencyRenderer, displayCurrency);
        }
    }
}
