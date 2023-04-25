using System.Collections;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private Transform _cameraMainView;
    [SerializeField] private Transform _cameraHeadView;
    [SerializeField] private Transform _cameraBodyView;
    [SerializeField] private Transform _cameraLegView;
    [SerializeField] private Transform _cameraWeaponsView;

    private Coroutine _translationTask;

    public void ResetPosition()
    {
        _camera.transform.position = _cameraMainView.position;
    }

    public void Translate(DetailType detailType)
    {
        switch(detailType)
        {
            case DetailType.Head:
                TranslateCamera(_cameraHeadView);
                break;
            case DetailType.Body:
                TranslateCamera(_cameraBodyView);
                break;
            case DetailType.Leg:
                TranslateCamera(_cameraLegView);
                break;
            case DetailType.Weapons:
                TranslateCamera(_cameraWeaponsView);
                break;
        }
    }

    private void TranslateCamera(Transform target)
    {
        if (_translationTask == null)
        {
            _translationTask = StartCoroutine(TranslateCameraTask(target.position));
        }
        else
        {
            StopCoroutine(_translationTask);
            _translationTask = StartCoroutine(TranslateCameraTask(target.position));
        }
    }

    private IEnumerator TranslateCameraTask(Vector3 position)
    {
        while (_camera.transform.position != position)
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, position, Time.deltaTime * _cameraSpeed);
            yield return null;
        }
    } 
}

public enum DetailType
{
    Head,
    Body,
    Leg,
    Weapons
}
