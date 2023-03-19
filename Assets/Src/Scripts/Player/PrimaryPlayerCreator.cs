using UnityEngine;

public class PrimaryPlayerCreator : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Leg _defaultLeg;
    [SerializeField] private Body _defaultBody;
    [SerializeField] private Head _defaultHead;
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private PlayerWallet _playerWallet;

    public void CreateDefaultPlayer()
    {
        _player.SetDetail(_defaultLeg);
        _player.SetDetail(_defaultBody);
        _player.SetDetail(_defaultHead);
        CreateDefaultWallet();
        CreateDefaultExperiecne();

        foreach (var weaponPlace in _player.WeaponPlaces)
        {
            _player.SetDetail(_defaultWeapon);
        }
    }

    private void CreateDefaultWallet()
    {
        _playerWallet.CreateDefault();
    }

    private void CreateDefaultExperiecne()
    {
        _player.SetExperience(new Experience(1, 0));
    }
}
