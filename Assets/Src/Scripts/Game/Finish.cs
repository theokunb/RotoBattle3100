using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private float _doorSpeed = 2;

    private Animator _animator;

    public event Action<Finish> LevelEnded;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        _animator.SetTrigger(SpaceShipAnimationController.Params.Open);
    }

    public void CloseDoor()
    {
        _animator.SetTrigger(SpaceShipAnimationController.Params.Close);
    }

    public void OnClosed()
    {
        LevelEnded?.Invoke(this);
    }
}
