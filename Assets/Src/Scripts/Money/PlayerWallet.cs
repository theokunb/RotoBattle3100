using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerWallet : MonoBehaviour
{
    private Wallet _wallet;
    private IWalletIncreaser _walletIncreaser => _wallet;
    private IWalletReducer _walletReducer => _wallet;

    public event Action ValueChanged;

    public Wallet Wallet => _wallet;

    public void SetWallet(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void Buy(DetailView item)
    {
        Pay(item.FullPrice);
        item.Detail.Unlock();
        
        Player player = GetComponent<Player>();
        player.AddItem(item.Detail.Id);

        GameStorage.Storage.Save(new PlayerData(GetComponent<Player>()));
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

    public void Add(IEnumerable<Currency> currincies)
    {
        foreach(var currency in currincies)
        {
            currency.Accept(_walletIncreaser);
        }
    }

    private void Pay(IEnumerable<Currency> currincies)
    {
        foreach(var currency in currincies)
        {
            currency.Accept(_walletReducer);
        }

        ValueChanged?.Invoke();
    }
}
