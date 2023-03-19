using System;

public class Player : Character
{
    private Experience _exp;

    public event Action ExperiencChanged;

    public int Level => _exp.Level;
    public int CurrentValue => _exp.CurrentValue;
    public int MaxValue => _exp.MaxValue;

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

    public void AddExperience(Enemy enemy)
    {
        _exp.AddExp(enemy);
        ExperiencChanged?.Invoke();
    }
}
