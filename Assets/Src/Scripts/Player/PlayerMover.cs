using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    private const float DeltaStick = 0.1f;
    private const float RotationTime = 1f;

    private PlayerInput _playerInput;
    private float _speed;
    private Character _character;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _character = GetComponent<Character>();
        _rigidbody= GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Start()
    {
        var leg = GetComponentInChildren<Leg>();
        _speed = leg != null ? leg.Speed : 0;
    }

    private void Update()
    {
        var value = _playerInput.PlayerMap.Move.ReadValue<Vector2>();
        _character.Moving?.Invoke(value.sqrMagnitude);

        if (value.sqrMagnitude < DeltaStick)
        {
            return;
        }

        Rotate(value);
        Move(value);
    }

    public void Suspend()
    {
        _character.Moving?.Invoke(0);
        _character.enabled = false;
    }

    public void Resume()
    {
        _character.enabled = true;
    }

    private void Rotate(Vector2 value)
    {
        float angle = Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, angle, 0)), RotationTime);
    }

    private void Move(Vector2 value)
    {
        Vector3 movement = new Vector3(value.x, 0, value.y);

        _rigidbody.MovePosition(transform.position + movement * _speed * Time.deltaTime);
    }
}
