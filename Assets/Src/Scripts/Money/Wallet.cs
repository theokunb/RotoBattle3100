using System;
using System.Collections.Generic;

[Serializable]
public class Wallet : IWallet
{
    private const int StartValue = 2000;

    private Currency _metal;
    private Currency _energy;
    private Currency _fuel;

    public Wallet()
    {
        _metal = new Metal(StartValue);
        _energy = new Energy(StartValue);
        _fuel= new Fuel(StartValue);
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
        Add((dynamic)currency);
    }

    public void Decrease(Currency currency)
    {
        Reduce((dynamic)currency);
    }

    public void Add(Metal metal)
    {
        _metal.Increase(metal);
    }

    public void Add(Energy energy)
    {
        _energy.Increase(energy);
    }

    public void Reduce(Metal metal)
    {
        _metal.Reduce(metal);
    }

    public void Reduce(Energy energy)
    {
        _energy.Reduce(energy);
    }

    public void Add(Fuel fuel)
    {
        _fuel.Increase(fuel);
    }

    public void Reduce(Fuel fuel)
    {
        _fuel.Reduce(fuel);
    }
}

public interface IWallet
{
    void Add(Metal metal);
    void Add(Energy energy);
    void Add(Fuel fuel);
    void Reduce(Metal metal);
    void Reduce(Energy energy);
    void Reduce(Fuel fuel);
}