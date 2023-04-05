using System;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PrimaryPlayerCreator _primaryCreator;
    [SerializeField] private ItemsPull _itemsPull;

    private void Awake()
    {
        ProccessPlayerData();
    }

    public bool TryLoadData(out PlayerData data)
    {
        data = GameStorage.Storage.Load();

        return data != null;
    }

    public void LoadPlayer(PlayerData playerData)
    {
        foreach (var detailData in playerData.GetDetailDatas())
        {
            var detail = FindDetail(detailData);
            _player.SetDetail((dynamic)detail);
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

    private void ProccessPlayerData()
    {
        if (TryLoadData(out PlayerData playerData))
        {
            Debug.Log("we got data");
            LoadPlayer(playerData);
        }
        else
        {
            Debug.Log("we not got data");
            _primaryCreator.CreateDefaultPlayer();
        }
    }
}
