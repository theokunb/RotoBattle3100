using System;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class Health : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;

    private int _maxHealth;
    private int _currentHealth;

    public event Action<Character> Die;

    public bool IsAlive => _currentHealth > 0;

    private void Start()
    {
        var character = GetComponent<Character>();
        _maxHealth = character.CalculateHealth();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.UpdateHealthBarValue(_maxHealth, _currentHealth);

        if (_currentHealth <= 0)
        {
            Die?.Invoke(GetComponent<Character>());
            gameObject.SetActive(false);
        }
    }
}
