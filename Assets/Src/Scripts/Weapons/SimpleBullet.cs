using UnityEngine;

public class SimpleBullet : Bullet
{
    protected override void Fly()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    protected override void Hit(Collider other)
    {
        if(other.TryGetComponent(out Character character))
        {
            var health = character.GetComponent<Health>();
            character.SetWhoAttacked(Owner);

            health.TakeDamage(Damage);
        }

        ResetBullet();
    }

    protected override void LifeTimeExpired()
    {
        ResetBullet();
    }
}
