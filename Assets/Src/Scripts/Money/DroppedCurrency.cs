using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DroppedCurrency : MonoBehaviour
{
    [SerializeField] private CurrencyType _currencyType;
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private DotweenAnimation _scaleAnimation;
    [SerializeField] private DotweenAnimation _rotationAnimation;

    private Price _price;
    public Currency Currency => _price.ToCurrency();

    private void Start()
    {
        int value = Random.Range(_minValue, _maxValue);
        _price = new Price(_currencyType, value);
        _scaleAnimation.Animate();
        _rotationAnimation.Animate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.GetComponent<Bag>().Put(this);
            gameObject.SetActive(false);
        }
    }
}
