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
        Instantiate(_explosion, transform.position, Quaternion.identity);

        var enemies = Physics.OverlapSphere(transform.position, _radius)
        .Where(collider => collider.TryGetComponent(out Character _) == true).ToList()
        .Select(collider => collider.GetComponent<Character>()).ToList()
        .Where(character => character.GetType() != Owner.GetType()).ToList()
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
