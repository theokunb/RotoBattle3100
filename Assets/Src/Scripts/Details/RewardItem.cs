using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _title;

    private PlayerInput _playerInput;

    public event Action<RewardItem> Tapped;

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

    public void Render(Detail detail)
    {
        _image.sprite = detail.GetComponent<DetailShop>().Icon;
        _title.text = detail.Title;
    }

    private void OnTouch(InputAction.CallbackContext ctx)
    {
        Tapped?.Invoke(this);
    }
}
