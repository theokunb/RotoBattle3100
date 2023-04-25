using System.Linq;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private float _radius;
    [SerializeField] private ParticleSystem _explosion;

    private void OnEnable()
    {
        Rigidbody.velocity = transform.forward * Speed * Time.deltaTime;
    }

    protected override void Fly()
    {
    }

    protected override void Hit(Collider other)
    {
        if(other.TryGetComponent(out Character _))
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
        Instantiate(_explosion, transform.position, Quaternion.identity);

        var colliders = Physics.OverlapSphere(transform.position, _radius).ToList();
        var enemies = colliders.Where(collider => collider.TryGetComponent(out Character _) == true)
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
