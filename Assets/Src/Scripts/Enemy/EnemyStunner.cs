using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStunner : Stunner
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    protected override void Resume()
    {
        _enemy.ResumeMovement();
        _enemy.enabled = true;
    }

    protected override void Suspend()
    {
        _enemy.SuspendMovement();
        _enemy.enabled = false;
    }
}
