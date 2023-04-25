using System;
using UnityEngine;

public class SpecialGood : MonoBehaviour
{
    [SerializeField] private Chest _chest;

    public event Action<Detail, DateTime> Opened;

    private void OnEnable()
    {
        _chest.Opened += OnChestOpened;
    }

    private void OnDisable()
    {
        _chest.Opened -= OnChestOpened;
    }

    public void SetLastDate(DateTime date)
    {
        _chest.SetLastOpenedDate(date);
    }

    private void OnChestOpened(Detail detail, DateTime dateTime)
    {
        Opened?.Invoke(detail, dateTime);
    }
}
