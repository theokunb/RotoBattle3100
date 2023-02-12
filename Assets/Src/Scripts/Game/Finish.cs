using System;
using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private float _doorSpeed = 2;

    private Coroutine _doorTask;
    private float _openedPosition;
    private float _closedPosition;

    public event Action<Finish> LevelEnded;

    private void Start()
    {
        _closedPosition = _door.transform.localScale.y / 2;
        _openedPosition = -_door.transform.localScale.y / 2;
    }

    public void OpenDoor()
    {
        if(_doorTask == null)
        {
            _doorTask = StartCoroutine(OpenDoorTask());
        }
        else
        {
            StopCoroutine(_doorTask);
            _doorTask = StartCoroutine(OpenDoorTask());
        }
    }

    public void CloseDoor()
    {
        if(_doorTask == null)
        {
            _doorTask = StartCoroutine(CloseDoorTask());
        }
        else
        {
            StopCoroutine(_doorTask);
            _doorTask = StartCoroutine(CloseDoorTask());
        }
    }

    private IEnumerator OpenDoorTask()
    {
        while (_door.transform.position.y > _openedPosition)
        {
            _door.transform.Translate(-_door.transform.up * _doorSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator CloseDoorTask()
    {
        while (_door.transform.position.y < _closedPosition)
        {
            _door.transform.Translate(_door.transform.up * _doorSpeed * Time.deltaTime);
            yield return null;
        }

        LevelEnded?.Invoke(this);
    }
}
