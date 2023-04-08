using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TerrainController : MonoBehaviour
{
    private const float FadeTime = 2;

    [SerializeField] private RectTransform _canvas;
    [SerializeField] private Image _visionCirecle;

    private Terrain _terrain;
    private Transform _playerTransform;

    private void Awake()
    {
        _terrain = GetComponent<Terrain>();
    }

    private void Start()
    {
        var color = _visionCirecle.color;
        color.a = 0;
        _visionCirecle.color = color;

        _canvas.localScale = new Vector3(_terrain.terrainData.size.x, _terrain.terrainData.size.z, 1);
    }

    private void FixedUpdate()
    {
        if (_playerTransform != null)
        {
            _visionCirecle.gameObject.transform.position = new Vector3(
                _playerTransform.position.x,
                _visionCirecle.gameObject.transform.position.y,
                _playerTransform.position.z);
        }
    }

    public void SetVisionSize(PlayerScanner playerScanner)
    {
        var rectTransf = _visionCirecle.GetComponent<RectTransform>();

        if (rectTransf != null)
        {
            rectTransf.sizeDelta =
                new Vector2(playerScanner.ScannerSize / _terrain.terrainData.size.x,
                playerScanner.ScannerSize / _terrain.terrainData.size.z);
        }

        _playerTransform = playerScanner.GetComponent<Transform>();
        FadeIn();
    }

    private void FadeIn()
    {
        _visionCirecle.DOFade(1, FadeTime);
    }

    public void VisionCircleFadeOut()
    {
        _visionCirecle.DOFade(0f, FadeTime);
    }
}
