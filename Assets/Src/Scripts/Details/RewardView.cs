using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RewardView : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event Action<RewardView> Tapped;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.PlayerMap.Touch.performed += ctx => OnTouch(ctx);
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.PlayerMap.Touch.performed -= ctx => OnTouch(ctx);
    }

    private void OnTouch(InputAction.CallbackContext ctx)
    {
        Tapped?.Invoke(this);
    }
}
