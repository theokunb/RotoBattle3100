using System.Collections.Generic;
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

        _player.SetExperience(new Experience(1, 0));
        _player.SetUpgrade(new Upgrade());
        _player.SetProgress(new PlayerProgress());
        _playerWallet.SetWallet(new Wallet());

        _player.SetUnlockedItems(new List<long>()
        {
            _defaultBody.Id,
            _defaultHead.Id,
            _defaultWeapon.Id,
            _defaultLeg.Id
        });

        foreach (var weaponPlace in _player.WeaponPlaces)
        {
            _player.SetDetail(_defaultWeapon);
        }
    }
}
