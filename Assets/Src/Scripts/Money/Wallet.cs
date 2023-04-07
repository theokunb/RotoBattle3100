using System;
using System.Collections.Generic;

[Serializable]
public class Wallet
{
    private Currency _metal;
    private Currency _energy;
    private Currency _fuel;

    public Wallet()
    {
        _metal = new Metal(0);
        _energy = new Energy(0);
        _fuel= new Fuel(0);
    }

    public Wallet(Currency metal, Currency energy, Currency fuel)
    {
        _metal = metal;
        _energy = energy;
        _fuel = fuel;
    }

    public Currency Metal => _metal;
    public Currency Energy => _energy;
    public Currency Fuel => _fuel;

    public IEnumerable<Currency> GetCurrencies()
    {
        yield return _metal;
        yield return _energy;
        yield return _fuel;
    }

    public void Increase(params Currency[] values)
    {
        foreach(var value in values)
        {
            Increase(value);
        }
    }

    public void Increase(IEnumerable<Currency> currencies)
    {
        foreach(var currency in currencies)
        {
            Increase(currency);
        }
    }

    public void Decrease(params Currency[] values)
    {
        foreach (var value in values)
        {
            Decrease(value);
        }
    }

    public void Decrease(IEnumerable<Currency> currencies)
    {
        foreach (var currency in currencies)
        {
            Decrease(currency);
        }
    }

    public void Increase(Currency currency)
    {
        if(currency is Metal)
        {
            Add(currency as Metal);
        }
        else if(currency is Energy)
        {
            Add(currency as Energy);
        }
        else if (currency is Fuel)
        {
            Add(currency as Fuel);
        }
    }

    public void Decrease(Currency currency)
    {
        if (currency is Metal)
        {
            Reduce(currency as Metal);
        }
        else if (currency is Energy)
        {
            Reduce(currency as Energy);
        }
        else if (currency is Fuel)
        {
            Reduce(currency as Fuel);
        }
    }

    private void Add(Metal metal)
    {
        _metal.Increase(metal);
    }

    private void Add(Energy energy)
    {
        _energy.Increase(energy);
    }

    private void Reduce(Metal metal)
    {
        _metal.Reduce(metal);
    }

    private void Reduce(Energy energy)
    {
        _energy.Reduce(energy);
    }

    private void Add(Fuel fuel)
    {
        _fuel.Increase(fuel);
    }

    private void Reduce(Fuel fuel)
    {
        _fuel.Reduce(fuel);
    }
}