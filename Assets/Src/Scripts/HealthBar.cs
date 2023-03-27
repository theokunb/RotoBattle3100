using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _foreground;
    [SerializeField] private Image _background;
    [SerializeField] private Color _color;
    [SerializeField] private float _changeValueSpeed = 2;

    private Camera _camera;

    private Coroutine _changevalueTask;

    private void Start()
    {
        _foreground.color = _color;
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position -_camera.transform.position);
    }

    public void Fade(bool status, float fadeTime)
    {
        _foreground.DOFade(Convert.ToInt32(status), fadeTime);
        _background.DOFade(Convert.ToInt32(status), fadeTime);
    }

    public void UpdateHealthBarValue(int maxHealth, int currentHealth)
    {
        var targetHealth = (float)currentHealth / maxHealth;

        if (_changevalueTask == null)
        {
            _changevalueTask = StartCoroutine(ChangeValueTask(targetHealth));
        }
        else
        {
            StopCoroutine(_changevalueTask);
            _changevalueTask = StartCoroutine(ChangeValueTask(targetHealth));
        }
    }

    private IEnumerator ChangeValueTask(float targetHealth)
    {
        while (_foreground.fillAmount != targetHealth)
        {
            _foreground.fillAmount = Mathf.MoveTowards(_foreground.fillAmount, targetHealth, _changeValueSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
