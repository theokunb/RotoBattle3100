using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerScanner : MonoBehaviour
{
    private List<Character> _enemies = new List<Character>();
    private Head _head;
    private Character _me;

    private void Awake()
    {
        _me = GetComponent<Character>();
    }

    private void OnEnable()
    {
        if (_head != null)
        {
            _head.EnemyDetected += OnEnemyDetected;
            _head.EnemyLost += OnEnemyLost;
        }
    }

    private void OnDisable()
    {
        _head.EnemyDetected -= OnEnemyDetected;
        _head.EnemyLost -= OnEnemyLost;
    }

    public Character GetNearestEnemy()
    {
        RemoveDied();

        if (_enemies.Count == 0)
            return null;

        List<KeyValuePair<float, Character>> enemiesOnDistance = new List<KeyValuePair<float, Character>>();

        foreach (var enemy in _enemies)
        {
            enemiesOnDistance.Add(new KeyValuePair<float, Character>(Vector3.Distance(transform.position, enemy.transform.position), enemy));
        }

        return enemiesOnDistance.OrderBy(element => element.Key).First().Value;
    }

    public void RemoveEnemy(Character character)
    {
        _enemies.Remove(character);
    }

    public void InitializeHead(Head head)
    {
        _head = head;
        OnEnable();
    }

    private void OnEnemyDetected(Character enemy)
    {
        if (_enemies.Contains(enemy) || _me.GetType() == enemy.GetType())
        {
            return;
        }

        _enemies.Add(enemy);
    }

    private void OnEnemyLost(Character enemy)
    {
        _enemies.Remove(enemy);
    }

    private void RemoveDied()
    {
        var aliveEnemies = _enemies.Where(character => character.GetComponent<Health>().IsAlive).ToList();
        _enemies = aliveEnemies;
    }
}
