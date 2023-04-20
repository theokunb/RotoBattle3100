using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StickController : MonoBehaviour
{
    [SerializeField] private GameObject _stick;

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
        FadeOutImages();
    }

    private void OnKeyPress(InputAction.CallbackContext context)
    {
        FadeInImages();
    }

    private void FadeInImages()
    {
        Image[] images = _stick.GetComponentsInChildren<Image>();

        foreach(var image in images)
        {
            image.DOFade(0, 0.5f).OnComplete(() =>
            {
                _stick.SetActive(false);
            });
        }
    }

    private void FadeOutImages()
    {
        Image[] images = _stick.GetComponentsInChildren<Image>();

        _stick.SetActive(true);

        foreach (var image in images)
        {
            image.DOFade(1, 0.5f);
        }
    }
}
