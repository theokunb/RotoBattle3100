using UnityEngine;

public class VisionCircle : MonoBehaviour
{
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private FadableImage _visionCirecle;

    private Terrain _terrain;
    private Transform _playerTransform;

    private void Awake()
    {
        _terrain = GetComponent<Terrain>();
    }

    private void Start()
    {
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

    public void VisionCircleFadeOut()
    {
        _visionCirecle.FadeOut();
    }

    private void FadeIn()
    {
        _visionCirecle.FadeIn();
    }
}
