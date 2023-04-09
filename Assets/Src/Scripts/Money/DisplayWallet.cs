using System.Linq;
using UnityEngine;

public class DisplayWallet : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private DisplayCurrency[] _displayPlaces;
    [SerializeField] private Sprite _metalIcon;
    [SerializeField] private Sprite _energyIcon;
    [SerializeField] private Sprite _fuelIcon;

    private ICurrencyRenderer _currencyRenderer;

    private void Awake()
    {
        _currencyRenderer = new CurrencyRenderer(_metalIcon, _energyIcon, _fuelIcon);
    }

    private void OnEnable()
    {
        _playerWallet.ValueChanged += OnCurrencyChanged;

        if (_playerWallet.Wallet != null)
        {
            RenderAll();
        }
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
        Currency[] currenciesInWallet = _playerWallet.Wallet.GetCurrencies().ToArray();

        for (int i = 0; i < currenciesInWallet.Length; i++)
        {
            Render(currenciesInWallet[i], _displayPlaces[i]);
        }
    }

    private void Render(Currency currency, DisplayCurrency displayCurrency)
    {
        currency.Accept(_currencyRenderer, displayCurrency);
    }
}

public class CurrencyRenderer : ICurrencyRenderer
{
    private Sprite _metalSprite;
    private Sprite _energySprite;
    private Sprite _fuelSprite;

    public CurrencyRenderer(Sprite metalSprite, Sprite energySprite, Sprite fuelSprite)
    {
        _metalSprite = metalSprite;
        _energySprite = energySprite;
        _fuelSprite = fuelSprite;
    }

    public void Render(DisplayCurrency displayCurrency, Metal metal)
    {
        displayCurrency.Render(_metalSprite, metal);
    }

    public void Render(DisplayCurrency displayCurrency, Energy energy)
    {
        displayCurrency.Render(_energySprite, energy);
    }

    public void Render(DisplayCurrency displayCurrency, Fuel fuel)
    {
        displayCurrency.Render(_fuelSprite, fuel);
    }
}

public interface ICurrencyRenderer
{
    void Render(DisplayCurrency displayCurrency, Metal metal);
    void Render(DisplayCurrency displayCurrency, Energy energy);
    void Render(DisplayCurrency displayCurrency, Fuel fuel);
}