using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class Body : RobotDetail
{
    [SerializeField] private LocalizedString _weaponsSlots;

    private List<Weapon> _weapons = new List<Weapon>();
    private WeaponPlace[] _weaponPlaces;

    public IEnumerable<WeaponPlace> WeaponPlaces => _weaponPlaces;

    private void Awake()
    {
        _weaponPlaces = GetComponentsInChildren<WeaponPlace>();
    }

    public void Attack(Character target)
    {
        foreach (var weapon in _weapons)
        {
            weapon.Shoot(target);
        }
    }

    public void TryAddWeapons(List<Weapon> newWeapons)
    {
        foreach (var newWeapon in newWeapons)
        {
            for (int i = 0; i < _weaponPlaces.Length; i++)
            {
                if (_weaponPlaces[i].IsBusy == false)
                {
                    _weapons.Add(Instantiate(newWeapon, _weaponPlaces[i].transform));
                    break;
                }
            }
        }

        newWeapons.Clear();
    }

    public override string GetSpecialStats()
    {
        return $"{_weaponsSlots.GetLocalizedString()}: {GetComponentsInChildren<WeaponPlace>().Length}";
    }

    public override void Accept(IDetailCreator creator, Transform parent)
    {
        creator.Create(this, parent);
    }
}
