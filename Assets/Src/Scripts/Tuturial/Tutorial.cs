using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System;

public abstract class Tutorial : MonoBehaviour
{
    [SerializeField] protected PlayerTutorial Player;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _pointer;
    [SerializeField] private GameObject[] _targets;
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _fadeValue;
    [SerializeField] private float _scaleValue;
    [SerializeField] private float _translationValue;

    private PlayerInput _input;
    private CanvasGroup _canvasGroup;
    private int _currentTip;
    private TweenerCore<Color, Color, ColorOptions> _fade;
    private TweenerCore<Vector3, Vector3, VectorOptions> _scale;
    private TweenerCore<Vector3, Vector3, VectorOptions> _translation;
    private TweenerCore<string, string, StringOptions> _textFilling;

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

        if (IsTutorialCompleted() == false)
        {
            FadePanel(0, 1, 0.5f, () =>
            {
                _currentTip = 0;
                BeginTutorial();
            });
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public abstract bool IsTutorialCompleted();

    public abstract void Completed();

    public void CalcelTutorial()
    {
        _textFilling.Kill();
        StopAnimation();

        Completed();
        gameObject.SetActive(false);
    }

    protected virtual void BeginTutorial()
    {
        ShowTip(_currentTip, _currentTip != 0);
    }

    private void OnKeyPress(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        StopAnimation();
        ShowTip(++_currentTip);
    }

    private void OnTouch(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        StopAnimation();
        ShowTip(++_currentTip);
    }

    private void FadePanel(float alphaFrom, float alphaTo, float time,Action onCompleteCallback)
    {
        _canvasGroup.alpha = alphaFrom;

        _canvasGroup.DOFade(alphaTo, time).SetUpdate(true).OnComplete(() =>
        {
            onCompleteCallback();
        });
    }

    private void ShowTip(int index, bool showPointer = true)
    {
        if(index >= _targets.Length)
        {
            FadePanel(1, 0, 0.5f, () =>
            {
                Completed();

                Player player = Player.GetComponent<Player>();
                GameStorage.Storage.Save(new PlayerData(player));

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

    private void StopAnimation()
    {
        _fade.Kill();
        _scale.Kill();
        _translation.Kill();
    }

    private void AnimatePointer()
    {
        var image = _pointer.GetComponent<Image>();
        Color color = new Color(image.color.r, image.color.g, image.color.b, 1);
        image.color = color;
        image.transform.localScale = Vector3.one;

        _fade = image.DOFade(_fadeValue, _animationDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(true);

        _scale = _pointer.transform.DOScale(_scaleValue, _animationDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(true);

        _translation = _pointer.transform
            .DOMoveX(_pointer.transform.position.x + Mathf.Cos(_pointer.transform.rotation.z) * _translationValue, _animationDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(true);
    }

    private void ShowTip(Tip tip)
    {
        if(tip == null)
        {
            return;
        }

        _text.text = string.Empty;
        _input.Disable();
        var time = tip.GetTip().Length / 40f;
        _textFilling = _text.DOText(tip.GetTip(), time)
            .SetRelative()
            .SetUpdate(true)
            .OnComplete(()=>
            {
                _input.Enable();
            });
    }
}
