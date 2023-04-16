using Agava.YandexGames;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameStorage
{
    public static IStorage Storage { get; private set; } = new YandexCloudStorage();
    public static ILeaderboardRecorder Leaderboard { get; private set; } = new YandexLeaderboard();
}

public class YandexLeaderboard : ILeaderboardRecorder
{
    public void Record(PlayerData data)
    {
        Leaderboard.GetPlayerEntry(LeaderboardTables.BestPlayers, (response) =>
        {
            if(response.score < data.PlayerLevel)
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

#if UNITY_WEBGL
        PlayerAccount.SetPlayerData(content);
#endif
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