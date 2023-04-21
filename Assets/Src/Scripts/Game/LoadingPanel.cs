using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private Image _progress;
    [SerializeField] private Sprite[] _wallpapers;
    [SerializeField] private Image _backgroundImage;

    public void Open(AsyncOperation operation)
    {
        int index = Random.Range(0, _wallpapers.Length);
        _backgroundImage.sprite = _wallpapers[index];

        StartCoroutine(LoadTask(operation));
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
