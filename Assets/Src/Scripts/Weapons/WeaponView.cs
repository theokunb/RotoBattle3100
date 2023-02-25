using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _removeButton;

    private Weapon _weapon;

    public event Action<WeaponView> RemoveClicked;

    public Weapon Weapon => _weapon;

    private void OnEnable()
    {
        _removeButton.onClick.AddListener(OnRemoveClicked);
    }

    private void OnDisable()
    {
        _removeButton.onClick.RemoveListener(OnRemoveClicked);
    }

    public void Render(DetailShop detailShop, Weapon weapon)
    {
        _weapon = weapon;
        _image.sprite = detailShop.Icon;
        _description.GetComponent<LocalizeStringEvent>().RefreshString();
    }

    private void OnRemoveClicked()
    {
        RemoveClicked?.Invoke(this);
    }
}
