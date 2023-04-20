using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
    private const int ExtraHealthPerLevel = 300;

    private List<string> _unlockedItems;

    public event Action HealthUpgraded;
    public event Action ShieldUpgraded;
    public event Action SpeedUpgraded;
    public event Action LevelChanged;
    public event Action ExperienceChanged;

    public Upgrade Upgrade { get; private set; }
    public PlayerProgress Progress { get; private set; }
    public Experience Experience { get; private set; }
    public IEnumerable<string> UnlockedItems => _unlockedItems;


    private void OnEnable()
    {
        if(Experience!= null)
        {
            Experience.LevelUp += OnLevelUp;
        }
    }

    private void OnDisable()
    {
        Experience.LevelUp -= OnLevelUp;
    }

    private void Start()
    {
        Experience.LevelUp += OnLevelUp;
        CorrectDetails();
    }

    public void Save()
    {
        GameStorage.Storage.Save(new PlayerData(this));
    }

    public void DropWeapon(Weapon weapon)
    {
        Destroy(weapon.gameObject);
    }

    public void SetExperience(Experience exp)
    {
        Experience = exp;
    }

    public void SetUpgrade(Upgrade upgrades)
    {
        Upgrade = upgrades;
    }

    public void SetProgress(PlayerProgress playerProgress)
    {
        Progress = playerProgress;
    }

    public void SetUnlockedItems(IEnumerable<string> unlockedItems)
    {
        _unlockedItems = new List<string>();

        foreach(var itemId in unlockedItems.Distinct())
        {
            _unlockedItems.Add(itemId);
        }
    }

    public void AddExperience(Enemy enemy)
    {
        Experience.AddExp(enemy);
        ExperienceChanged?.Invoke();
    }

    public void AddUpgrade(Upgrades upgrade)
    {
        Upgrade.AddUpgrade(upgrade);

        switch (upgrade)
        {
            case Upgrades.Health:
                HealthUpgraded?.Invoke();
                break;
            case Upgrades.Speed:
                SpeedUpgraded?.Invoke();
                break;
            case Upgrades.Shield:
                ShieldUpgraded?.Invoke();
                break;
            default:
                break;
        }
    }

    public override int CalculateHealth()
    {
        int healthCount = Upgrade.GetUpgradesCount(Upgrades.Health);

        return base.CalculateHealth() + healthCount * ExtraHealthPerLevel;
    }

    public void AddItem(string itemKey)
    {
        _unlockedItems?.Add(itemKey);
    }

    private void OnLevelUp()
    {
        LevelChanged?.Invoke();
    }
}
