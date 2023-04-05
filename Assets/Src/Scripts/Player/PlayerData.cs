using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    [JsonProperty("DetailDatas")] private List<DetailData> _detailDatas = new List<DetailData>();
    [JsonProperty("Wallet")] private Wallet _wallet = new Wallet();
    [JsonProperty("Exp")] private Experience _exp;
    [JsonProperty("Upgrade")] private Upgrade _upgrade;
    [JsonProperty("PlayerProgress")] private PlayerProgress _playerProgress;

    public PlayerData(Player player)
    {
        foreach (var detail in player.GetAllDetails())
        {
            _detailDatas.Add(detail);
        }

        _exp = new Experience(player.Level, player.CurrentValue);
        _wallet = player.GetComponent<PlayerWallet>().Wallet;
        _upgrade = player.Upgrade;
        _playerProgress = player.Progress;
    }

    public IEnumerable<DetailData> GetDetailDatas() => _detailDatas;
    public Wallet GetWallet() => _wallet;
    public Experience GetExperience() => _exp;
    public Upgrade GetUpgrades() => _upgrade;
    public PlayerProgress GetProgress() => _playerProgress;
}
