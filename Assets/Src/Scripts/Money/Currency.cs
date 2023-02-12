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
}

[Serializable]
public class Metal : Currency
{
    private const string CurrencyTitle = "метал";

    public Metal(int count) : base(count)
    {
    }

    public override string Title => CurrencyTitle;
}

[Serializable]
public class Energy : Currency
{
    private const string CurrencyTitle = "энерния";

    public Energy(int count) : base(count)
    {
    }

    public override string Title => CurrencyTitle;
}

[Serializable]
public class Fuel : Currency
{
    private const string CurrencyTitle = "топливо";

    public Fuel(int count) : base(count)
    {
    }

    public override string Title => CurrencyTitle;
}