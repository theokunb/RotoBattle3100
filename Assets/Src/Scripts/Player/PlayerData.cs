using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int Metal;
    public int Energy;
    public int Fuel;
    public int PlayerLevel;
    public int PlayerExperience;
    public int CompletedLevels;
    public List<long> EquipedDetails;
    public List <long> UnlockedDetails;
    public List<Upgrades> Upgrades;
    public DateTime LastGamedDay;

    public PlayerData(Player player)
    {
        var wallet = player.GetComponent<PlayerWallet>().Wallet;
        FillWallet(wallet);
        FillProgress(player.Progress);
        FillExperience(player.Experience);
        FillEquipedDetails(player.GetAllDetails());
        FillUnlockedDetails(player.UnlockedItems);
        FillUpgrades(player.Upgrade.GetUpgrades());
    }

    public PlayerData() { }

    public Wallet GetWallet() => new Wallet(new Metal(Metal), new Energy(Energy), new Fuel(Fuel));
    public Experience GetExperience() => new Experience(PlayerLevel, PlayerExperience);
    public Upgrade GetUpgrades() => new Upgrade(Upgrades.ToArray());
    public PlayerProgress GetProgress() => new PlayerProgress(CompletedLevels, LastGamedDay);

    public void CorrectingData(PrimaryPlayerCreator creator)
    {
        if(PlayerLevel == 0)
        {
            FillExperience(creator.CreateDefaultExperience());
        }
        if(EquipedDetails == null)
        {
            FillEquipedDetails(creator.CreateDefaultDetails());
        }
        if(UnlockedDetails== null)
        {
            FillUnlockedDetails(creator.CreateDefaultDetails());
        }
        if(Upgrades == null)
        {
            FillUpgrades(creator.CreateDefaultUpgrades());
        }
        if(Upgrades.Count >= PlayerLevel)
        {
            Upgrades.RemoveRange(PlayerLevel, Upgrades.Count - 1);
        }
    }

    private void FillEquipedDetails(IEnumerable<long> detailsId)
    {
        EquipedDetails = new List<long>();

        foreach (var detail in detailsId)
        {
            EquipedDetails.Add(detail);
        }
    }

    private void FillUpgrades(IEnumerable<Upgrades> upgrades)
    {
        Upgrades = new List<Upgrades>();

        foreach (var upgrade in upgrades)
        {
            Upgrades.Add(upgrade);
        }
    }

    private void FillWallet(Wallet wallet)
    {
        Metal = wallet.MetalCount;
        Energy = wallet.EnergyCount;
        Fuel = wallet.FuelCount;
    }

    private void FillExperience(Experience experience)
    {
        PlayerLevel = experience.Level;
        PlayerExperience = experience.CurrentValue;
    }

    private void FillProgress(PlayerProgress playerProgress)
    {
        CompletedLevels = playerProgress.GetCompletedLevels();
        LastGamedDay = playerProgress.GetLastGamedDay();
    }

    private void FillUnlockedDetails(IEnumerable<long> detailsId)
    {
        UnlockedDetails = new List<long>();

        foreach(var detailId in detailsId)
        {
            UnlockedDetails.Add(detailId);
        }
    }
}
