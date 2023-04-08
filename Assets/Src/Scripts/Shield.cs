using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Player))]
public class Shield : MonoBehaviour
{
    private const float FadeTime = 0.2f;
    private const int AbsorbPerLevel = 100;

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float _shieldUpdateTime;

    private float _elapsedTime = 0;
    private int _currentValue;
    private int _maxValue;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.ShieldUpgraded += OnShieldUpgraded;
    }

    private void OnDisable()
    {
        _player.ShieldUpgraded -= OnShieldUpgraded;
    }

    private void Start()
    {
        CreateShield(FadeTime);
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _shieldUpdateTime)
        {
            CreateShield(FadeTime);
            _elapsedTime = 0;
        }
    }

    private void OnShieldUpgraded()
    {
        CreateShield(FadeTime);
    }

    private void CreateShield(float fadeDuration = 0)
    {
        int shieldsCount = _player.Upgrade.GetUpgradesCount(Upgrades.Shield);
        _maxValue = shieldsCount * AbsorbPerLevel;

        _currentValue = _maxValue;
        _healthBar?.UpdateHealthBarValue(_maxValue, _currentValue);
        _healthBar?.Fade(_currentValue > 0, fadeDuration);
    }

    public int AbsorbDamage(int damage)
    {
        if (_currentValue <= 0)
        {
            return damage;
        }

        _currentValue -= damage;
        _healthBar.UpdateHealthBarValue(_maxValue, _currentValue);

        if (_currentValue <= 0)
        {
            _healthBar.Fade(_currentValue > 0, FadeTime);
            return Mathf.Abs(_currentValue);
        }
        else
        {
            return 0;
        }
    }
}
