using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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
        UnlockDetails(_player.UnlockedItems);
    }

    public void LoadPlayer(PlayerData playerData)
    {
        foreach (var detailData in playerData.EquipedDetails)
        {
            var detail = FindDetail(detailData);
            _player.SetDetail(detail);
        }

        _player.SetExperience(playerData.GetExperience());
        _player.SetUpgrade(playerData.GetUpgrades());
        _player.GetComponent<PlayerWallet>().SetWallet(playerData.GetWallet());
        _player.SetProgress(playerData.GetProgress());
        _player.SetUnlockedItems(playerData.UnlockedDetails);
    }

    private void UnlockDetails(IEnumerable<long> detailsId)
    {
        foreach(var id in detailsId)
        {
            _itemsPull.Details.Where(detail => detail.Id == id).FirstOrDefault()?.Unlock();
        }
    }

    private Detail FindDetail(long detailId)
    {
        foreach (var item in _itemsPull.Details)
        {
            if (item.Id == detailId)
            {
                return item;
            }
        }

        return null;
    }

    private void MakePlayer(string json)
    {
        if (json == string.Empty || json == "{}" || json == "null")
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
