using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ElectricCharge : Bullet
{
    [SerializeField] private float _disableTime;

    private bool _isHit;

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
            StartCoroutine(DisableTask(character));
            _isHit = true;
        }
        else
        {
            ResetBullet();
        }
    }

    protected override void LifeTimeExpired()
    {
        if (_isHit == false)
        {
            ResetBullet();
        }
    }

    private IEnumerator DisableTask(Character character)
    {
        character.enabled = false;
        float elapsedTime = 0;

        while(elapsedTime < _disableTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        character.enabled = true;
        ResetBullet();
    }
}
