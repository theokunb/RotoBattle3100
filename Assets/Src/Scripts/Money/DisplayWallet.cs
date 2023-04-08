using UnityEngine;

public class DisplayWallet : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private DisplayCurrency _currencyMetal;
    [SerializeField] private DisplayCurrency _currencyEnergy;
    [SerializeField] private DisplayCurrency _currencyFuel;
    [SerializeField] private Sprite _metalIcon;
    [SerializeField] private Sprite _energyIcon;
    [SerializeField] private Sprite _fuelIcon;

    private void OnEnable()
    {
        _playerWallet.ValueChanged += OnCurrencyChanged;

    }

    private void OnDisable()
    {
        _playerWallet.ValueChanged -= OnCurrencyChanged;
    }

    private void Start()
    {
        RenderAll();
    }

    private void OnCurrencyChanged()
    {
        RenderAll();
    }

    private void RenderAll()
    {
        foreach (var currency in _playerWallet.Wallet.GetCurrencies())
        {
            Render(currency);
        }
    }

    private void Render(Currency currency)
    {
        if(currency is Metal)
        {
            _currencyMetal.Render(_metalIcon, currency as Metal);
        }
        else if (currency is Energy)
        {
            _currencyEnergy.Render(_energyIcon, currency as Energy);
        }
        else if(currency is Fuel)
        {
            _currencyFuel.Render(_fuelIcon, currency as Fuel);
        }
    }
}
