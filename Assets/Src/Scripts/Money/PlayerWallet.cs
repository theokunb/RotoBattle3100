using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private Wallet _wallet;

    public event Action<Wallet> ValueChanged;

    public Wallet Wallet => _wallet;

    public void CreateDefault()
    {
        _wallet = new Wallet();
    }

    public void SetWallet(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void Buy(DetailView item)
    {
        Pay(item.FullPrice);
        item.Detail.Unlock();

        GameStorage.Save(new PlayerData(GetComponent<Player>()), GameStorage.PlayerData);
    }

    public bool CanBuy(IEnumerable<Currency> fullPrice)
    {
        foreach(var elementOfPrice in fullPrice)
        {
            var currency = _wallet.GetCurrencies().Where(element => element.Title == elementOfPrice.Title).FirstOrDefault();

            if(elementOfPrice.Count > currency.Count)
            {
                return false;
            }
        }

        return true;
    }

    private void Pay(IEnumerable<Currency> currincies)
    {
        _wallet.Decrease(currincies);
        ValueChanged?.Invoke(_wallet);
    }
}
