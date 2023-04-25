using Agava.YandexGames;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameStorage
{
    public static IStorage Storage { get; private set; } = new YandexCloudStorage();
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
        GameLeaderboard.Leaderboard.Record(data);
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