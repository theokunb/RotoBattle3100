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
    private Coroutine _regenerationTask;

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

        int regeneration = GetRegenerationValue();
        _regenerationTask = StartCoroutine(StartRegeneration(regeneration));
    }

    private IEnumerator StartRegeneration(int value)
    {
        float _elapsedTime = 0;
        float _regenerationDelay = 1;

        while (true)
        {
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime >= _regenerationDelay)
            {
                _elapsedTime = 0;
                _currentHealth += value;

                if(_currentHealth > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }

                _healthBar?.UpdateHealthBarValue(_maxHealth, _currentHealth);
            }

            yield return null;
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
