using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private Button _closeButton;
    [SerializeField] private LocalizeStringEvent _weaponsStatus;
    [SerializeField] private GameObject _weaponContainer;

    private int _allWeaponsCount = 0;
    private int _currentWeaponsCount = 0;

    public string AllWeaponsCount => _allWeaponsCount.ToString();
    public string CurrentWeaponsCount => _currentWeaponsCount.ToString();

    private void OnEnable()
    {
        ClearView();
        _closeButton.onClick.AddListener(CloseButtonClicked);

        foreach (Weapon weapon in _player.WeaponPlaces.Select(weaponPlace => weaponPlace.CurrentWeapon))
        {
            if (weapon != null)
            {
                WeaponView weaponView = Instantiate(_template, _weaponContainer.transform);
                weaponView.Render(weapon.GetComponent<DetailShop>(), weapon);
                weaponView.RemoveClicked += OnRemoveClicked;
            }
        }

        _allWeaponsCount = _player.WeaponPlaces.Count();
        _currentWeaponsCount = _player.WeaponPlaces.Where(weapon => weapon.IsBusy == false).Count();
        _weaponsStatus.RefreshString();
    }

    private void CloseButtonClicked()
    {
        GameStorage.Storage.Save(new PlayerData(_player));
        gameObject.SetActive(false);
    }

    private void OnRemoveClicked(WeaponView weaponView)
    {
        _player.DropWeapon(weaponView.Weapon);
        _currentWeaponsCount++;
        weaponView.RemoveClicked -= OnRemoveClicked;

        _weaponsStatus.RefreshString();

        Destroy(weaponView.gameObject);
    }

    private void ClearView()
    {
        foreach(Transform child in _weaponContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
