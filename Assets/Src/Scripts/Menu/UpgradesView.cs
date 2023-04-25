using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesView : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerData;
    [SerializeField] private TMP_Text _healthCount;
    [SerializeField] private TMP_Text _shieldCount;
    [SerializeField] private TMP_Text _speedCount;
    [SerializeField] private Button[] _upgradeButtons;
    [SerializeField] private TMP_Text _freeUpgrades;
    [SerializeField] private Button _resetButton;

    public event Action Reset;

    private void OnEnable()
    {
        _resetButton.onClick.AddListener(OnResetClicked);
    }

    private void OnDisable()
    {
        _resetButton?.onClick.RemoveListener(OnResetClicked);
    }

    public void SetPlayerData(string data)
    {
        _playerData.text = data;
    }

    public void SetHealthCount(int count)
    {
        _healthCount.text = count.ToString();
    }

    public void SetShieldCount(int count)
    {
        _shieldCount.text = count.ToString();
    }

    public void SetSpeedCount(int count)
    {
        _speedCount.text = count.ToString();
    }

    public void SetAvailableUpgrades(string text,int wasted, int maxCount)
    {
        int free = maxCount - wasted;

        if(free <= 0)
        {
            foreach(var button in _upgradeButtons)
            {
                button.interactable = false;
            }
        }
        else
        {
            foreach(var button in _upgradeButtons)
            {
                button.interactable = true;
            }
        }

        _freeUpgrades.text = $"{text}: {free}";
    }

    private void OnResetClicked()
    {
        Reset?.Invoke();
    }
}
