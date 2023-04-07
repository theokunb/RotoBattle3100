using Newtonsoft.Json;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PrimaryPlayerCreator _primaryCreator;
    [SerializeField] private ItemsPull _itemsPull;

    private void Awake()
    {
        string content = PlayerPrefs.GetString(PlayerPrefsKeys.PlayerData, string.Empty);

        MakePlayer(content);
    }

    public void LoadPlayer(PlayerData playerData)
    {
        foreach (var detailData in playerData.GetDetailDatas())
        {
            var detail = FindDetail(detailData);
            _player.SetDetail(detail);
        }

        _player.SetExperience(playerData.GetExperience());
        _player.SetUpgrade(playerData.GetUpgrades());
        _player.GetComponent<PlayerWallet>().SetWallet(playerData.GetWallet());
        _player.SetProgress(playerData.GetProgress());
    }

    private Detail FindDetail(DetailData detailData)
    {
        foreach (var item in _itemsPull.Details)
        {
            if (item.Id == detailData.Id)
            {
                return item;
            }
        }

        return null;
    }

    private void MakePlayer(string json)
    {
        if (json == string.Empty || json == "{}")
        {
            _primaryCreator.CreateDefaultPlayer();
        }
        else
        {
            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            LoadPlayer(playerData);
        }
    }
}
