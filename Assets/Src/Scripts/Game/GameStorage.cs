using Agava.YandexGames;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameStorage
{
    public static IStorage Storage { get; private set; } = new FileStorage();
    public static ILeaderboardRecorder Leaderboard { get; private set; } = new MyLeaderboard();
    public static Ad Ad { get; private set; } = new Ad();

}

public class Ad
{
    private readonly int _adDelay = 4;
    private int _current = 0;
    
    public void ShowInterstitial(Action OnCloseCallback)
    {
        _current++;

        if(_current >= _adDelay)
        {
            _current = 0;

            InterstitialAd.Show(onCloseCallback: (status) =>
            {
                OnCloseCallback();
            });
        }
        else
        {
            OnCloseCallback();
        }
    }

    public void ShowRewardVideo(Action rewardCallback)
    {
        bool isRewarded = false;

        VideoAd.Show(onRewardedCallback: () =>
        {
            isRewarded = true;
        },
        onCloseCallback: () =>
        {
            if(isRewarded)
            {
                rewardCallback();
            }
        });
    }
}

public class MyLeaderboard : ILeaderboardRecorder
{
    public void Record(PlayerData data)
    {

    }
}

public class YandexLeaderboard : ILeaderboardRecorder
{
    public void Record(PlayerData data)
    {
        Leaderboard.GetPlayerEntry(LeaderboardTables.BestPlayers, (response) =>
        {
            if(response == null || response.score < data.PlayerLevel)
            {
                Leaderboard.SetScore(LeaderboardTables.BestPlayers, data.PlayerLevel);
            }
        });
    }
}

public class YandexCloudStorage : IStorage
{
    public PlayerData Load()
    {
        return null;
    }

    public void Save(PlayerData data)
    {
        string content = JsonConvert.SerializeObject(data);

        PlayerPrefs.SetString(PlayerPrefsKeys.PlayerData, content);
        PlayerAccount.SetPlayerData(content);
        GameStorage.Leaderboard.Record(data);
    }
}

public class FileStorage : IStorage
{
    public PlayerData Load()
    {
        string path = Application.persistentDataPath + "/playerDetails.dat";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = (PlayerData)formatter.Deserialize(stream);
            stream.Close();

            return playerData;
        }
        else
        {
            return null;
        }
    }

    public void Save(PlayerData data)
    {
        string content = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(PlayerPrefsKeys.PlayerData, content);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerDetails.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);

        stream.Close();
    }
}

public interface IStorage
{
    PlayerData Load();
    void Save(PlayerData data);
}

public interface ILeaderboardRecorder
{
    void Record(PlayerData data);
}