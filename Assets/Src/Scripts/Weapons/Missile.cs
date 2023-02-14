using System.Linq;
using UnityEngine;

public class Missile : Bullet
{
    [SerializeField] private float _radius;
    [SerializeField] private ParticleSystem _explosion;

    protected override void Fly()
    {
        Rigidbody.MovePosition(transform.position + transform.forward * Speed * Time.deltaTime);
    }

    protected override void Hit(Collider other)
    {
        Explode();
    }

    protected override void LifeTimeExpired()
    {
        Explode();
    }

    private void Explode()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);

        var enemies = Physics.OverlapSphere(transform.position, _radius)
            .Where(collider => collider.TryGetComponent(out Character _) == true)
            .Select(collider => collider.GetComponent<Character>())
            .Where(character => character.GetType() != Owner.GetType())
            .ToArray();

        foreach(var enemy in enemies)
        {
            var health = enemy.GetComponent<Health>();
            enemy.SetWhoAttacked(Owner);
            health.TakeDamage(Damage);
        }

        ResetBullet();
    }
}
