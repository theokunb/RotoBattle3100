using UnityEngine;

public class WeaponPlace : MonoBehaviour
{
    public Weapon CurrentWeapon => GetComponentInChildren<Weapon>();

    public bool IsBusy => CurrentWeapon != null;
}
