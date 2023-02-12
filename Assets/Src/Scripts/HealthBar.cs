using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Color _color;
    [SerializeField] private float _changeValueSpeed = 2;

    private Camera _camera;

    private Coroutine _changevalueTask;

    private void Start()
    {
        _healthBar.color = _color;
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position -_camera.transform.position);
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
        while (_healthBar.fillAmount != targetHealth)
        {
            _healthBar.fillAmount = Mathf.MoveTowards(_healthBar.fillAmount, targetHealth, _changeValueSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
