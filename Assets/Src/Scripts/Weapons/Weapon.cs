using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : Detail
{
    private const int BulletsCount = 8;
    private const string Label = "оружие";
    private const string DamageLabel = "наносимый урон:";
    private const string SpeedLabel = "скорость:";

    [SerializeField] private int _damage;
    [SerializeField] private float _maxDelayBetweenShoot;
    [SerializeField] private float _minDelayBetweenShoot;
    [SerializeField] private Transform _shootPlace;
    [SerializeField] private Bullet _template;
    [SerializeField] private float _bulletSpeed;

    private Character _owner;
    private float _elapsedTime;
    private List<Bullet> _bullets;
    private float _delayBetweenShoot;

    public int Damage => _damage;

    private void Start()
    {
        _owner = GetComponentInParent<Character>();
        _bullets = new List<Bullet>();
        _delayBetweenShoot = Random.Range(_minDelayBetweenShoot, _maxDelayBetweenShoot);

        for (int i = 0; i < BulletsCount; i++)
        {
            var newBullet = Instantiate(_template, _shootPlace);
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
            bullet.transform.LookAt(target.transform);
            bullet.gameObject.SetActive(true);
        }

        _elapsedTime = 0;
    }

    public override string GetLabel()
    {
        return Label;
    }

    public override string GetStats()
    {
        return $"{DamageLabel} {Damage}\n{SpeedLabel} {GetSpeedLabel(_bulletSpeed)}";
    }

    private string GetSpeedLabel(float speed)
    {
        const string low = "низкая";
        const string medium = "средняя";
        const string high = "высокая";

        Dictionary<string, float> speedDictionary = new Dictionary<string, float>
        {
            { low, 1f },
            { medium, 0.5f },
            { high, 0f }
        };

        return speedDictionary.Where(element => speed > element.Value).First().Key;
    }
}
