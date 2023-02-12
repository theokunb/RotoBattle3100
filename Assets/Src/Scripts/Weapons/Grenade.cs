using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : Bullet
{
    [SerializeField] private float _radius;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rigidbody.velocity = transform.forward * Speed * Time.deltaTime;
    }

    protected override void Fly()
    {
        
    }

    protected override void Hit(Collider other)
    {
        if(other.TryGetComponent<Character>(out Character _))
        {
            Explode();
        }
    }

    protected override void LifeTimeExpired()
    {
        Explode();
    }

    private void Explode()
    {
        var enemies = Physics.OverlapSphere(transform.position, _radius)
            .Where(collider => collider.TryGetComponent(out Character _) == true)
            .Select(collider => collider.GetComponent<Character>())
            .Where(character => character.GetType() != Owner.GetType())
            .ToArray();

        foreach (var enemy in enemies)
        {
            var health = enemy.GetComponent<Health>();
            enemy.SetWhoAttacked(Owner);
            health.TakeDamage(Damage);
        }

        ResetBullet();
    }
}
