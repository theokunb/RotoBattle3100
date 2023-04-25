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
    public List<string> EquipedDetails;
    public List <string> UnlockedDetails;
    public List<Upgrades> Upgrades;
    public DateTime LastGamedDay;
    public bool IsMenuTutorialCompleted;
    public bool IsGameTutorialCompleted;

    public PlayerData(Player player)
    {
        var wallet = player.GetComponent<PlayerWallet>().Wallet;
        FillWallet(wallet);
        FillProgress(player.Progress);
        FillExperience(player.Experience);
        FillEquipedDetails(player.GetAllDetails());
        FillUnlockedDetails(player.UnlockedItems);
        FillUpgrades(player.Upgrade.GetUpgrades());
        IsMenuTutorialCompleted = player.GetComponent<PlayerTutorial>().IsMenuTutorialCompleted;
        IsGameTutorialCompleted = player.GetComponent<PlayerTutorial>().IsGameTutorialCompleted;
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
        if(EquipedDetails == null || EquipedDetails.Count == 0)
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
            while(Upgrades.Count >= PlayerLevel)
            {
                Upgrades.RemoveAt(Upgrades.Count - 1);
            }
        }
    }

    private void FillEquipedDetails(IEnumerable<string> detailsId)
    {
        EquipedDetails = new List<string>();

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
        LastGamedDay = playerProgress.LastGamedDay;
    }

    private void FillUnlockedDetails(IEnumerable<string> detailsId)
    {
        UnlockedDetails = new List<string>();

        foreach(var detailId in detailsId)
        {
            UnlockedDetails.Add(detailId);
        }
    }
}
