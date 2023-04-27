using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const int RegenerationPerLevel = 20;

    [SerializeField] private HealthBar _healthBar;

    private int _maxHealth;
    private int _currentHealth;
    private Shield _shield;
    private Character _character;
    private int _regenerationValue = 0;
    private float _elapsedTime = 0;
    private float _regenerationDelay = 1;

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

        _regenerationValue = GetRegenerationValue();
    }

    private void FixedUpdate()
    {
        if(_regenerationValue == 0)
        {
            return;
        }

        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _regenerationDelay)
        {
            _elapsedTime = 0;
            _currentHealth += _regenerationValue;

            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            _healthBar?.UpdateHealthBarValue(_maxHealth, _currentHealth);
        }
    }

    private int GetRegenerationValue()
    {
        if(_character is Player)
        {
            Player player = _character as Player;

            return RegenerationPerLevel * player.Upgrade.GetUpgradesCount(Upgrades.Health);
        }
        else
        {
            return 0;
        }
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
