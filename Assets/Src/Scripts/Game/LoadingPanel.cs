using Agava.YandexGames;
using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private Image _progress;
    [SerializeField] private Sprite[] _wallpapers;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private BackgroundAudioPlayer _backgroundAudio;

    public void Open(AsyncOperation operation)
    {
        operation.allowSceneActivation = false;
        _backgroundAudio.Pause();
        int index = Random.Range(0, _wallpapers.Length);
        _backgroundImage.sprite = _wallpapers[index];

        GameStorage.Ad.ShowInterstitial(() =>
        {
            _backgroundAudio.Resume();
            operation.allowSceneActivation = true;
            StartCoroutine(LoadTask(operation));
        });
    }

    private IEnumerator LoadTask(AsyncOperation operation)
    {
        while (operation.isDone == false)
        {
            _progress.fillAmount = operation.progress;

            yield return null;
        }
    }
}
