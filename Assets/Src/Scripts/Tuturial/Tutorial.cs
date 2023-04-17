using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _pointer;
    [SerializeField] private GameObject[] _targets;
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _fadeValue;
    [SerializeField] private float _scaleValue;
    [SerializeField] private float _translationValue;

    private PlayerInput _input;
    private CanvasGroup _canvasGroup;
    private int _currentTip = 0;
    private TweenerCore<Color, Color, ColorOptions> _fade;
    private TweenerCore<Vector3, Vector3, VectorOptions> _scale;
    private TweenerCore<Vector3, Vector3, VectorOptions> _translation;

    private void Awake()
    {
        _input = new PlayerInput();
        _canvasGroup = GetComponent<CanvasGroup>();

        _input.PlayerMap.Touch.performed += OnTouch;
        _input.PlayerMap.KeyPress.performed += OnKeyPress;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Start()
    {
        ShowTip(_currentTip, _currentTip != 0);
    }

    private void OnKeyPress(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _fade.Kill();
        _scale.Kill();
        _translation.Kill();
        ShowTip(++_currentTip);
    }

    private void OnTouch(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _fade.Kill();
        _scale.Kill();
        _translation.Kill();
        ShowTip(++_currentTip);
    }

    private void ShowTip(int index, bool showPointer = true)
    {
        if(index >= _targets.Length)
        {
            _canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
        else
        {
            var tip = _targets[index].GetComponentInChildren<Tip>();

            _pointer.SetActive(showPointer);
            ShowTip(tip);

            if(showPointer == true)
            {
                SetOnTarget(tip);
            }
        }
    }

    private void SetOnTarget(Tip tip)
    {
        if(tip == null)
        {
            return;
        }

        _pointer.transform.position = tip.transform.position;
        _pointer.transform.rotation = tip.transform.rotation;

        AnimatePointer();
    }

    private void AnimatePointer()
    {
        var image = _pointer.GetComponent<Image>();
        Color color = new Color(image.color.r, image.color.g, image.color.b, 1);
        image.color = color;
        image.transform.localScale = Vector3.one;

        _fade = image.DOFade(_fadeValue, _animationDuration).SetLoops(-1, LoopType.Yoyo);
        _scale = _pointer.transform.DOScale(_scaleValue, _animationDuration).SetLoops(-1, LoopType.Yoyo);
        _translation = _pointer.transform
            .DOMoveX(_pointer.transform.position.x + Mathf.Cos(_pointer.transform.rotation.z) * _translationValue, _animationDuration)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void ShowTip(Tip tip)
    {
        if(tip == null)
        {
            return;
        }

        _text.text = string.Empty;
        _input.Disable();
        var time = tip.GetTip().Length / 30f;

        _text.DOText(tip.GetTip(), time).SetRelative().OnComplete(() =>
        {
            _input.Enable();
        });
    }
}
