using UnityEngine;

public class SimpleBullet : Bullet
{
    protected override void Fly()
    {
        Rigidbody.MovePosition(transform.position + transform.forward * Speed * Time.deltaTime);
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
