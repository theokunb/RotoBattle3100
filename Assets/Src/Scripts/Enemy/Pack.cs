using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pack
{
    [SerializeField] private Enemy[] _enemies;

    private List<Enemy> _createdEnemies;

    public event Action<Character> EnemyDied;

    public void Create(float position, float width)
    {
        var step = width / _enemies.Length;
        _createdEnemies = new List<Enemy>();

        for (int i = 0; i < _enemies.Length; i++)
        {
            var createdEnemy = _enemies[i].Create(new Vector2(step / 2 + i * step, position));
            createdEnemy.EnemyDetected += OnEnemyDetected;
            var health = createdEnemy.GetComponent<Health>();
            health.Die += OnDie;

            _createdEnemies.Add(createdEnemy);
        }
    }

    public int GetCount() => _enemies.Length;

    public void SubscribeOnDetection()
    {
        foreach(var enemy in _createdEnemies)
        {
            enemy.EnemyDetected += OnEnemyDetected;
        }
    }

    public void UnsubscribeOnDetection()
    {
        foreach (var enemy in _createdEnemies)
        {
            enemy.EnemyDetected -= OnEnemyDetected;
        }
    }

    private void OnEnemyDetected(Character character)
    {
        foreach (var enemy in _createdEnemies)
        {
            enemy.Target = character;
        }
    }

    private void OnDie(Character character)
    {
        character.GetComponent<Health>().Die -= OnDie;
        EnemyDied?.Invoke(character);
    }
}
