using System;

public class Player : Character
{
    private const int ExtraHealthPerLevel = 300;

    private Experience _exp;
    private Upgrade _upgrade;

    public event Action ExperiencChanged;
    public event Action LevelChanged;
    public event Action HealthUpgraded;
    public event Action ShieldUpgraded;
    public event Action SpeedUpgraded;

    public int Level => _exp.Level;
    public int CurrentValue => _exp.CurrentValue;
    public int MaxValue => _exp.MaxValue;
    public Upgrade Upgrade => _upgrade;

    private void OnEnable()
    {
        _exp.LevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        _exp.LevelUp -= OnLevelUp;
    }

    private void Start()
    {
        CorrectDetails(LegPosition);
        Save();
    }

    public void Save()
    {
        GameStorage.Save(new PlayerData(this), GameStorage.PlayerData);
    }

    public void DropWeapon(Weapon weapon)
    {
        Destroy(weapon.gameObject);
    }

    public void SetExperience(Experience exp)
    {
        _exp = exp;
    }

    public void SetUpgrade(Upgrade upgrades)
    {
        _upgrade = upgrades;
    }

    public void AddExperience(Enemy enemy)
    {
        _exp.AddExp(enemy);
        ExperiencChanged?.Invoke();
    }

    public void AddUpgrade(Upgrades upgrade)
    {
        _upgrade.AddUpgrade(upgrade);

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

    private void OnLevelUp()
    {
        LevelChanged?.Invoke();
    }
}
