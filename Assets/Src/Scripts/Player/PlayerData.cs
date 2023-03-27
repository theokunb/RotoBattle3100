using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    private List<DetailData> _detailDatas = new List<DetailData>();
    private Wallet _wallet = new Wallet();
    private Experience _exp;
    private Upgrade _upgrade;

    public PlayerData(Player player)
    {
        foreach (var detail in player.GetAllDetails())
        {
            _detailDatas.Add(detail);
        }

        _exp = new Experience(player.Level, player.CurrentValue);
        _wallet = player.GetComponent<PlayerWallet>().Wallet;
        _upgrade = player.Upgrade;
    }

    public IEnumerable<DetailData> DetailDatas => _detailDatas;
    public Wallet Wallet => _wallet;
    public Experience Experience => _exp;
    public Upgrade Upgrades => _upgrade;
}
