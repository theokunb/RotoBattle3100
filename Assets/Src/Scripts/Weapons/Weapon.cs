using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;

[RequireComponent(typeof(WeaponSound))]
public class Weapon : Detail
{
    private const int BulletsCount = 5;
    private const float AttackSpeedPerLevel = 0.9f;

    [SerializeField] private int _damage;
    [SerializeField] private float _maxDelayBetweenShoot;
    [SerializeField] private float _minDelayBetweenShoot;
    [SerializeField] private Transform _shootPlace;
    [SerializeField] private Bullet _template;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private LocalizedString _damageLabel;
    [SerializeField] private LocalizedString _attackSpeedLabel;

    private Character _owner;
    private float _elapsedTime;
    private List<Bullet> _bullets;
    private float _delayBetweenShoot;
    private WeaponSound _weaponSound;

    public int Damage => _damage;

    private void Start()
    {
        _weaponSound = GetComponent<WeaponSound>();
        _owner = GetComponentInParent<Character>();
        _bullets = new List<Bullet>();

        CalculateDelayBetweenShoot();

        for (int i = 0; i < BulletsCount; i++)
        {
            var newBullet = Instantiate(_template);
            newBullet.Initialize(_owner, _damage, _bulletSpeed);
            newBullet.gameObject.SetActive(false);
            _bullets.Add(newBullet);
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    public void Shoot(Character target)
    {
        if (_delayBetweenShoot > _elapsedTime)
        {
            return;
        }

        var bullet = _bullets.Where(element => element.gameObject.activeSelf == false).FirstOrDefault();

        if (bullet == null)
        {
            return;
        }
        else
        {
            bullet.transform.position = _shootPlace.position;
            bullet.transform.rotation = transform.rotation;
            bullet.gameObject.SetActive(true);
            _weaponSound.Play();
        }

        _elapsedTime = 0;
        CalculateDelayBetweenShoot();
    }

    public override string GetStats()
    {
        return $"{_damageLabel.GetLocalizedString()}: {Damage}\n{_attackSpeedLabel.GetLocalizedString()}: {GetSpeedLabel(_maxDelayBetweenShoot)}";
    }

    private string GetSpeedLabel(float speed)
    {
        LocalizedString low = new LocalizedString("DetailStats", "LowAttackSpeed");
        LocalizedString medium = new LocalizedString("DetailStats", "MediumAttackSpeed");
        LocalizedString high = new LocalizedString("DetailStats", "HighAttackSpeed");

        Dictionary<string, float> speedDictionary = new Dictionary<string, float>
        {
            { low.GetLocalizedString(), 2f },
            { medium.GetLocalizedString(), 0.7f },
            { high.GetLocalizedString(), 0f }
        };

        return speedDictionary.Where(element => speed > element.Value).First().Key;
    }

    private void CalculateDelayBetweenShoot()
    {
        _delayBetweenShoot = Random.Range(_minDelayBetweenShoot, _maxDelayBetweenShoot);

        if (_owner is Player)
        {
            var upgradesCount = (_owner as Player).Upgrade.GetUpgradesCount(Upgrades.Speed);

            _delayBetweenShoot *= Mathf.Pow(AttackSpeedPerLevel, upgradesCount);
        }
    }

    public override void Accept(IDetailCreator creator, Transform parent)
    {
        creator.Create(this, parent);
    }
}
