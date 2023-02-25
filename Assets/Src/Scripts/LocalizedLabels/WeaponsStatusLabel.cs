using UnityEngine;
using UnityEngine.Localization.Components;

public class WeaponsStatusLabel : MonoBehaviour
{
    [SerializeField] private WeaponManager _weaponManager;

    public WeaponManager WeaponManager => _weaponManager;
}
