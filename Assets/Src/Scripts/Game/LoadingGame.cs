using Agava.YandexGames;
using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    [SerializeField] private GameObject _loadminScreen;
    [SerializeField] private Image _loadingProgress;

    public IEnumerator Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        yield return YandexGamesSdk.Initialize(() =>
        {
            Debug.Log("sdk inited");
        });
#endif
        yield return LoadGameAsync();
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
