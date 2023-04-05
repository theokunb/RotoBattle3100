using Agava.YandexGames;
using Newtonsoft.Json;
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
        Debug.Log("trying get data");

        PlayerData playerData = null;

        PlayerAccount.GetPlayerData((data) =>
        {
            Debug.Log($"data: {data}");
            playerData = JsonConvert.DeserializeObject<PlayerData>(data);
        },
        (error) =>
        {
            Debug.Log($"error: {error}");
        });

        return playerData;
    }

    public void Save(PlayerData data)
    {
        Debug.Log("trying set data");

        string content = JsonConvert.SerializeObject(data);

        PlayerAccount.SetPlayerData(content, () =>
        {
            Debug.Log($"saved: {content}");
        },
        (error) =>
        {
            Debug.Log($"error: {error}");
        });
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
            return default;
        }
    }

    public void Save(PlayerData data)
    {
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
