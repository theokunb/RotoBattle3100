using UnityEngine;

public class Reward : MonoBehaviour
{
    private const int AllProbability = 100;
    private const int CurrencyDropChance = 60;

    [SerializeField] private ItemsPull _dropItems;
    [SerializeField] private CurrencyPull _currencyPull;

    public GameObject GetReward()
    {
        int dropChance = 20;

        int roll = Random.Range(0, AllProbability);

        if(roll > dropChance)
        {
            return null;
        }
        else
        {
            roll = Random.Range(0, AllProbability);

            if(roll > CurrencyDropChance)
            {
                return GetRandomCurrency();
            }
            else
            {
                return GetRandomItem();
            }
        }
    }

    private GameObject GetRandomItem()
    {
        return _dropItems?.GetRandomDetail()?.gameObject;
    }

    private GameObject GetRandomCurrency()
    {
        var createdCurrency = _currencyPull.GetRandomCurrency();

        if(createdCurrency == null)
        {
            return null;
        }

        return Instantiate(createdCurrency).gameObject;
    }
}
