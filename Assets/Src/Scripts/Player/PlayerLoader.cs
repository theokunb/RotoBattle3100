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

    private void SetupPlayer(PlayerData playerData)
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
        _player.SetUnlockedItems(playerData.UnlockedDetails.Distinct());

        PlayerTutorial playerTutorial = _player.GetComponent<PlayerTutorial>();
        playerTutorial.IsMenuTutorialCompleted = playerData.IsMenuTutorialCompleted;
        playerTutorial.IsGameTutorialCompleted = playerData.IsGameTutorialCompleted;
    }

    private void UnlockDetails(IEnumerable<string> detailsKey)
    {
        foreach (string id in detailsKey)
        {
            Detail detail = _itemsPull.Details.Where(detail => detail.Id == id).FirstOrDefault();
            detail?.Unlock();
        }

        //foreach (var detail in _itemsPull.Details)
        //{
        //    detail.Unlock();
        //}
    }

    private Detail FindDetail(string detailKey)
    {
        foreach (var item in _itemsPull.Details)
        {
            if (item.Id == detailKey)
            {
                return item;
            }
        }

        return null;
    }

    private void MakePlayer(string json)
    {
        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(json);

        if(playerData == null)
        {
            playerData= new PlayerData();
        }

        playerData.CorrectingData(_primaryCreator);
        SetupPlayer(playerData);
    }
}
