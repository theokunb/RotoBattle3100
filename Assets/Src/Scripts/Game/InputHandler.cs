using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private FadableImage _circle;
    [SerializeField] private FadableImage _stick;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        _playerInput.PlayerMap.Touch.started += OnTouch;
        _playerInput.PlayerMap.KeyPress.started += OnKeyPress;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.PlayerMap.Touch.started -= OnTouch;
        _playerInput.PlayerMap.KeyPress.started -= OnKeyPress;
        _playerInput.Disable();
    }

    private void OnTouch(InputAction.CallbackContext context)
    {
        _circle.FadeIn();
        _stick.FadeIn();
    }

    private void OnKeyPress(InputAction.CallbackContext context)
    {
        _circle.FadeOut();
        _stick.FadeOut();
    }
}
