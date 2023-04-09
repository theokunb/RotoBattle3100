using System.Collections.Generic;

public class Wallet : IWalletIncreaser, IWalletReducer
{
    private Currency _metal;
    private Currency _energy;
    private Currency _fuel;

    public Wallet()
    {
        _metal = new Metal(0);
        _energy = new Energy(0);
        _fuel = new Fuel(0);
    }

    public Wallet(Currency metal, Currency energy, Currency fuel)
    {
        _metal = metal;
        _energy = energy;
        _fuel = fuel;
    }

    public int MetalCount => _metal.Count;
    public int EnergyCount => _energy.Count;
    public int FuelCount => _fuel.Count;

    public IEnumerable<Currency> GetCurrencies()
    {
        yield return _metal;
        yield return _energy;
        yield return _fuel;
    }

    public void Increase(Metal metal)
    {
        _metal.Increase(metal);
    }

    public void Increase(Energy energy)
    {
        _energy.Increase(energy);
    }

    public void Increase(Fuel fuel)
    {
        _fuel.Increase(fuel);
    }

    public void Reduce(Metal metal)
    {
        metal.Reduce(metal);
    }

    public void Reduce(Energy energy)
    {
        _energy.Reduce(energy);
    }

    public void Reduce(Fuel fuel)
    {
        _fuel.Reduce(fuel);
    }
}

public interface IWalletIncreaser
{
    void Increase(Metal metal);
    void Increase(Energy energy);
    void Increase(Fuel fuel);
}

public interface IWalletReducer
{
    void Reduce(Metal metal);
    void Reduce(Energy energy);
    void Reduce(Fuel fuel);
}