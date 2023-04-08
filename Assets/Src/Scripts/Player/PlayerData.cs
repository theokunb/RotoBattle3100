using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class PlayerData
{
    public List<DetailData> DetailDatas = new List<DetailData>();
    public int Metal;
    public int Energy;
    public int Fuel;
    public int PlayerLevel;
    public int PlayerExperience;
    public List<Upgrades> Upgrades = new List<Upgrades>();
    public int CompletedLevels;
    public DateTime LastGamedDay;

    public PlayerData(Player player)
    {
        foreach (var detail in player.GetAllDetails())
        {
            DetailDatas.Add(detail);
        }

        Upgrades = player.Upgrade.GetUpgrades().ToList();

        var playerWallet = player.GetComponent<PlayerWallet>().Wallet;
        Metal = playerWallet.Metal.Count;
        Energy = playerWallet.Energy.Count;
        Fuel = playerWallet.Fuel.Count;

        PlayerLevel = player.Experience.Level;
        PlayerExperience = player.Experience.CurrentValue;

        CompletedLevels = player.Progress.GetCompletedLevels();
        LastGamedDay = player.Progress.GetLastGamedDay();
    }

    public PlayerData() { }

    public IEnumerable<DetailData> GetDetailDatas() => DetailDatas;
    public Wallet GetWallet() => new Wallet(new Metal(Metal), new Energy(Energy), new Fuel(Fuel));
    public Experience GetExperience() => new Experience(PlayerLevel, PlayerExperience);
    public Upgrade GetUpgrades() => new Upgrade(Upgrades.ToArray());
    public PlayerProgress GetProgress() => new PlayerProgress(CompletedLevels, LastGamedDay);
}
