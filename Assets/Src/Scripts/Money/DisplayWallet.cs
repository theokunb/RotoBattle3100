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

    private Wallet _wallet;

    private void OnEnable()
    {
        if(_wallet != null)
        {
            RenderAll();
        }

        _playerWallet.ValueChanged += OnCurrencyChanged;
    }

    private void OnDisable()
    {
        _playerWallet.ValueChanged -= OnCurrencyChanged;
    }

    public void Start()
    {
        _wallet = _playerWallet.Wallet;

        RenderAll();
    }

    private void OnCurrencyChanged(Wallet wallet)
    {
        RenderAll();
    }

    private void RenderAll()
    {
        foreach (var currency in _wallet.GetCurrencies())
        {
            Render(currency);
        }
    }

    private void Render(Currency currency)
    {
        Render((dynamic)currency);
    }

    private void Render(Metal metal)
    {
        _currencyMetal.Render(_metalIcon, metal);
    }

    private void Render(Energy energy)
    {
        _currencyEnergy.Render(_energyIcon, energy);
    }

    private void Render(Fuel fuel)
    {
        _currencyFuel.Render(_fuelIcon, fuel);
    }
}
