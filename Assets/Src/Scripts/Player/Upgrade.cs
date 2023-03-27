using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Upgrade
{
    private List<Upgrades> _upgrades;

    

    public Upgrade()
    {
        _upgrades = new List<Upgrades>();
    }

    public Upgrade(params Upgrades[] upgrades)
    {
        _upgrades = new List<Upgrades>();

        foreach (var upgrade in upgrades)
        {
            _upgrades.Add(upgrade);
        }
    }

    public void AddUpgrade(Upgrades upgrade)
    {
        _upgrades.Add(upgrade);
    }

    public int GetUpgradesCount(Upgrades upgrade)
    {
        return _upgrades.Where(element => element == upgrade).Count();
    }
}

[Serializable]
public enum Upgrades
{
    Health,
    Shield,
    Speed
}