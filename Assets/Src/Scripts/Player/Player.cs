public class Player : Character
{
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
}
