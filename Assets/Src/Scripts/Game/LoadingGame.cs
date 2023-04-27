using Agava.YandexGames;
using IJunior.TypedScenes;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    [SerializeField] private GameObject _loadminScreen;
    [SerializeField] private Image _loadingProgress;

    public IEnumerator Start()
    {
        //PlayerPrefs.DeleteAll();

#if UNITY_WEBGL && !UNITY_EDITOR
        yield return YandexGamesSdk.Initialize(() =>
        {
            Debug.Log("sdk inited");
            PlayerAccount.GetPlayerData((data) =>
            {
                PlayerPrefs.SetString(PlayerPrefsKeys.PlayerData, data);
                StartCoroutine(LoadGameAsync());
            },
            (error) =>
            {
                StartCoroutine(LoadGameAsync());
            });
        });
#endif


#if UNITY_EDITOR
        PlayerData playerData = GameStorage.Storage.Load();
        string data = JsonConvert.SerializeObject(playerData);

        PlayerPrefs.SetString(PlayerPrefsKeys.PlayerData, data);
        yield return LoadGameAsync();
#endif

        yield return null;
    }

    private IEnumerator LoadGameAsync()
    {
        AsyncOperation operation = MenuScene.LoadAsync();

        while(operation.isDone == false)
        {
            _loadingProgress.fillAmount = operation.progress;

            yield return null;
        }
    }
}
