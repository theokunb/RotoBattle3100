using UnityEngine;

public class VertixBullet : Bullet
{
    [SerializeField] private float _radius;
    [SerializeField] private float _rotationSpeed;

    protected override void Fly()
    {
        var angle = new Vector3(0, _rotationSpeed, 0) * Time.deltaTime;

        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * _radius, Speed * Time.deltaTime);
        transform.Rotate(angle);
    }

    protected override void Hit(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            var health = character.GetComponent<Health>();
            character.SetWhoAttacked(Owner);

            health.TakeDamage(Damage);
        }
    }

    protected override void LifeTimeExpired()
    {
        transform.localScale = Vector3.one;
        ResetBullet();
    }
}
