using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerStunner : Stunner
{
    private PlayerMover _playerMover;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    protected override void Resume()
    {
        _playerMover.enabled = true;
        _playerMover.Resume();
    }

    protected override void Suspend()
    {
        _playerMover.Suspend();
        _playerMover.enabled = false;
    }
}
