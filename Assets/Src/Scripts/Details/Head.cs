using System;
using UnityEngine;
using UnityEngine.Localization;

[RequireComponent(typeof(CapsuleCollider))]
public class Head : RobotDetail
{
    private const int ScannerHeight = 15;

    [SerializeField] private float _scannerRadius;
    [SerializeField] private LocalizedString _visionRange;

    private CapsuleCollider _scannerCollider;

    public event Action<Character> EnemyDetected;
    public event Action<Character> EnemyLost;

    public float ScannerRadius => _scannerCollider.radius;

    private void Awake()
    {
        _scannerCollider = GetComponent<CapsuleCollider>();
        _scannerCollider.radius = _scannerRadius * (1f / transform.localScale.y);
        _scannerCollider.height = ScannerHeight * (1f / transform.localScale.x);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Character enemy))
        {
            EnemyDetected?.Invoke(enemy);

            var health = enemy.GetComponent<Health>();
            health.Die += OnEnemyDie;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Character enemy))
        {
            EnemyLost?.Invoke(enemy);
        }
    }

    private void OnEnemyDie(Character enemy)
    {
        var health = enemy.GetComponent<Health>();
        health.Die -= OnEnemyDie;

        EnemyLost?.Invoke(enemy);
    }

    public override string GetSpecialStats()
    {
        return $"{_visionRange.GetLocalizedString()}: {_scannerRadius}m";
    }

    public override void Accept(IDetailCreator visitor, Transform parent)
    {
        visitor.Create(this, parent);
    }
}
