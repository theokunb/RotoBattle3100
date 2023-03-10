using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DroppedCurrency : MonoBehaviour
{
    [SerializeField] private CurrencyType _currencyType;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;

    private BoxCollider _boxCollider;
    private Price _price;
    private Coroutine _animationTask;

    public Sprite Icon => _icon;
    public Currency Currency => _price.ToCurrency();

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    private void Start()
    {
        int value = Random.Range(_minValue, _maxValue);
        _price = new Price(_currencyType, value);
        _animationTask = StartCoroutine(AnimationTask());
    }

    private IEnumerator AnimationTask()
    {
        transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);

        while (true)
        {
            transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.GetComponent<Bag>().Put(this);
            StopCoroutine(_animationTask);
            gameObject.SetActive(false);
        }
    }
}
