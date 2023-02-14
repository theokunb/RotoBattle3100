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
    }

    protected override void Suspend()
    {
        _enemy.SuspendMovement();
    }
}
