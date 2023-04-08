using System;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class Health : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;

    private int _maxHealth;
    private int _currentHealth;
    private Shield _shield;
    private Character _character;

    public event Action<Character> Die;

    public bool IsAlive => _currentHealth > 0;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        if (_character is Player)
        {
            (_character as Player).HealthUpgraded += SetHealth;
        }
    }

    private void OnDisable()
    {
        if (_character is Player)
        {
            (_character as Player).HealthUpgraded -= SetHealth;
        }
    }

    private void Start()
    {
        _shield = GetComponent<Shield>();
        SetHealth();
    }

    private void SetHealth()
    {
        _maxHealth = _character.CalculateHealth();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_shield != null)
        {
            damage = _shield.AbsorbDamage(damage);
        }

        _currentHealth -= damage;
        _healthBar.UpdateHealthBarValue(_maxHealth, _currentHealth);

        if (_currentHealth <= 0)
        {
            Die?.Invoke(GetComponent<Character>());
            gameObject.SetActive(false);
        }
    }
}
