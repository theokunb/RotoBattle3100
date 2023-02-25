using UnityEngine;

public class WeaponLabel : MonoBehaviour
{
    [SerializeField] private WeaponView _weaponView;

    public string Description => $"{_weaponView?.Weapon?.Title}\n{_weaponView?.Weapon?.GetStats()}";
}
