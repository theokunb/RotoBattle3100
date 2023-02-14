using System.Collections;
using UnityEngine;

public class ElectricCharge : Bullet
{
    [SerializeField] private float _disableTime;

    protected override void Fly()
    {
        Rigidbody.MovePosition(transform.position + transform.forward * Speed * Time.deltaTime);
    }

    protected override void Hit(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            var health = character.GetComponent<Health>();
            character.SetWhoAttacked(Owner);

            health.TakeDamage(Damage);
            var stunner = character.GetComponent<Stunner>();
            stunner?.Disable(_disableTime);
        }

        ResetBullet();
    }

    protected override void LifeTimeExpired()
    {
        ResetBullet();
    }
}
