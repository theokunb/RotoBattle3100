using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    private const float DeltaStick = 0.1f;
    private const float RotationTime = 1f;

    private PlayerInput _playerInput;
    private float _speed;
    private Rigidbody _rigidbody;
    private Player _player;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _player.SpeedUpgraded += SetSpeed;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _player.SpeedUpgraded -= SetSpeed;
    }

    private void Start()
    {
        SetSpeed();
    }

    private void FixedUpdate()
    {
        var value = _playerInput.PlayerMap.Move.ReadValue<Vector2>();
        _player.Moving?.Invoke(value.sqrMagnitude);

        if (value.sqrMagnitude < DeltaStick)
        {
            return;
        }

        Rotate(value);
        Move(value);
    }

    private void SetSpeed()
    {
        var leg = GetComponentInChildren<Leg>();
        float baseSpeed = leg != null ? leg.Speed : 0;

        _speed = baseSpeed + GetBonusSpeed();
    }

    private float GetBonusSpeed()
    {
        const float SpeedPerLevel = 0.1f;

        int upgradesCount = _player.Upgrade.GetUpgradesCount(Upgrades.Speed);

        return upgradesCount * SpeedPerLevel;
    }

    public void Suspend()
    {
        _player.Moving?.Invoke(0);
        _player.enabled = false;
    }

    public void Resume()
    {
        _player.enabled = true;
    }

    private void Rotate(Vector2 value)
    {
        float angle = Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, angle, 0)), RotationTime);
    }

    private void Move(Vector2 value)
    {
        Vector3 movement = new Vector3(value.x, 0, value.y) * Time.deltaTime;

        _rigidbody.MovePosition(transform.position + movement * _speed);
    }
}
