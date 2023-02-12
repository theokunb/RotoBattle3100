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
