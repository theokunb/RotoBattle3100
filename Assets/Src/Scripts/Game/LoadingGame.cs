using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    [SerializeField] private GameObject _loadminScreen;
    [SerializeField] private Image _loadingProgress;
    
    private const float LoadingTime = 1f;

    public void Start()
    {
        StartCoroutine(LoadGameAsync());
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
