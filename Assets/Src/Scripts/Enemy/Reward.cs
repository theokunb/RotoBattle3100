using UnityEngine;

public class Reward : MonoBehaviour
{
    private const int AllProbability = 100;
    private const int DropChance = 40;
    private const int ItemDropChance = 35;

    [SerializeField] private ItemsPull _dropItems;
    [SerializeField] private CurrencyPull _currencyPull;

    public GameObject GetReward()
    {
        int roll = Random.Range(0, AllProbability);

        if(roll > DropChance)
        {
            return null;
        }
        else
        {
            roll = Random.Range(0, AllProbability);

            if(roll > ItemDropChance)
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
        return _dropItems?.CreateRandomDetail()?.gameObject;
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
