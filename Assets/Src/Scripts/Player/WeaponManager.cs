using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    private const string LabelStatus = "свободных слотов:";

    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _weaponsStatus;
    [SerializeField] private GameObject _weaponContainer;

    private int _allWeaponsCount;
    private int _currentWeaponsCount;

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
        DisplayStatus();
    }

    private void CloseButtonClicked()
    {
        GameStorage.Save(new PlayerData(_player), GameStorage.PlayerData);
        gameObject.SetActive(false);
    }

    private void OnRemoveClicked(WeaponView weaponView)
    {
        _player.DropWeapon(weaponView.Weapon);
        _currentWeaponsCount++;
        weaponView.RemoveClicked -= OnRemoveClicked;

        Destroy(weaponView.gameObject);
        DisplayStatus();
    }

    private void DisplayStatus()
    {
        _weaponsStatus.text = $"{LabelStatus} {_currentWeaponsCount}/{_allWeaponsCount}";
    }

    private void ClearView()
    {
        foreach(Transform child in _weaponContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
