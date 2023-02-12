using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData 
{
    private List<DetailData> _detailDatas= new List<DetailData>();
    private Wallet _wallet = new Wallet();

    public PlayerData(Player player)
    {
        foreach(var detail in player.GetAllDetails())
        {
            _detailDatas.Add(detail);
        }

        _wallet = player.GetComponent<PlayerWallet>().Wallet;
    }

    public IEnumerable<DetailData> DetailDatas => _detailDatas;
    public Wallet Wallet => _wallet;
}
