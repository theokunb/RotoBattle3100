using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Spawner _spawner;
    private int _maxCount = 0;
    private int _died = 0;

    public event Action LevelCompleted;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
    }

    private void OnEnable()
    {
        _spawner.EnemyAdded += OnEnemyAdded;
        _spawner.EnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        _spawner.EnemyAdded -= OnEnemyAdded;
        _spawner.EnemyDied -= OnEnemyDied;
    }

    private void OnEnemyAdded(int count)
    {
        _maxCount += count;
        DisplayCounter();
    }

    private void OnEnemyDied()
    {
        _died++;
        DisplayCounter();

        if(_died == _maxCount)
        {
            LevelCompleted?.Invoke();
        }
    }

    private void DisplayCounter()
    {
        _text.text = $"{_maxCount - _died}/{_maxCount}";
    }
}
