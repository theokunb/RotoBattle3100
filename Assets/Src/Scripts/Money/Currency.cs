using System;

[Serializable]
public abstract class Currency
{
    private int _count;

    protected Currency(int count)
    {
        _count = count;
    }

    public int Count => _count;
    public abstract string Title { get; }

    public void Increase(Currency currency)
    {
        if (currency != null)
        {
            _count += currency.Count;
        }
    }

    public void Reduce(Currency currency)
    {
        if (currency != null)
        {
            _count -= currency.Count;
        }
    }

    public abstract void Accept(IWalletIncreaser increaser);
    public abstract void Accept(IWalletReducer reducer);
    public abstract void Accept(ICurrencyRenderer renderer, DisplayCurrency displayCurrency);
}

[Serializable]
public class Metal : Currency
{
    private const string CurrencyTitle = "метал";

    public Metal(int count) : base(count)
    {
    }

    public override string Title => CurrencyTitle;

    public override void Accept(IWalletIncreaser increaser)
    {
        increaser.Increase(this);
    }

    public override void Accept(IWalletReducer reducer)
    {
        reducer.Reduce(this);
    }

    public override void Accept(ICurrencyRenderer renderer, DisplayCurrency displayCurrency)
    {
        renderer.Render(displayCurrency, this);
    }
}

[Serializable]
public class Energy : Currency
{
    private const string CurrencyTitle = "энерния";

    public Energy(int count) : base(count)
    {
    }

    public override string Title => CurrencyTitle;

    public override void Accept(IWalletIncreaser increaser)
    {
        increaser.Increase(this);
    }
    public override void Accept(IWalletReducer reducer)
    {
        reducer.Reduce(this);
    }

    public override void Accept(ICurrencyRenderer renderer, DisplayCurrency displayCurrency)
    {
        renderer.Render(displayCurrency, this);
    }
}

[Serializable]
public class Fuel : Currency
{
    private const string CurrencyTitle = "топливо";

    public Fuel(int count) : base(count)
    {
    }

    public override string Title => CurrencyTitle;

    public override void Accept(IWalletIncreaser increaser)
    {
        increaser.Increase(this);
    }
    public override void Accept(IWalletReducer reducer)
    {
        reducer.Reduce(this);
    }

    public override void Accept(ICurrencyRenderer renderer, DisplayCurrency displayCurrency)
    {
        renderer.Render(displayCurrency, this);
    }
}