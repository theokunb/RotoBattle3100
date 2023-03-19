using System.Linq;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PrimaryPlayerCreator _primaryCreator;
    [SerializeField] private ItemsPull _itemsPull;

    private PlayerProgress _playerProgress;

    public PlayerProgress PlayerProgress => _playerProgress;

    private void Awake()
    {
        ProccessPlayerData();
        ProccessPlayerProgress();
    }

    public bool TryLoadData<T>(out T data,string fileName)
    {
        data = GameStorage.LoadData<T>(fileName);

        return data != null;
    }

    public void LoadPlayer(PlayerData playerData)
    {
        foreach (var detailData in playerData.DetailDatas)
        {
            var detail = FindDetail(detailData);
            _player.SetDetail((dynamic)detail);
        }

        _player.SetExperience(playerData.Experience);
        _player.GetComponent<PlayerWallet>().SetWallet(playerData.Wallet);
    }

    private Detail FindDetail(DetailData detailData)
    {
        foreach(var item in _itemsPull.Details)
        {
            if(item.Id == detailData.Id)
            {
                return item;
            }
        }

        return null;
    }

    private void ProccessPlayerData()
    {
        if (TryLoadData(out PlayerData playerData, GameStorage.PlayerData))
        {
            LoadPlayer(playerData);
        }
        else
        {
            _primaryCreator.CreateDefaultPlayer();
        }
    }

    private void ProccessPlayerProgress()
    {
        if(TryLoadData(out _playerProgress, GameStorage.PlayerProgress))
        {

        }
        else
        {
            _playerProgress = new PlayerProgress();
            GameStorage.Save(_playerProgress, GameStorage.PlayerProgress);
        }

        _playerProgress.VisitGame();
    }
}
