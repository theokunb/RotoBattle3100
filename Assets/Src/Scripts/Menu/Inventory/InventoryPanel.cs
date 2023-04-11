using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraController))]
[RequireComponent(typeof(InventoryObserver))]
public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject _scrollView;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _headButton;
    [SerializeField] private Button _bodyButton;
    [SerializeField] private Button _legButton;
    [SerializeField] private Button _weaponsButton;
    [SerializeField] private WeaponManager _weaponManager;
    [SerializeField] private Button _playerWeapons;

    private CameraController _cameraController;
    private InventoryObserver _inventoryObserver;

    private void Start()
    {
        _cameraController = GetComponent<CameraController>();
        _inventoryObserver = GetComponent<InventoryObserver>();
        _headButton.onClick.Invoke();
    }

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClicked);
        _headButton.onClick.AddListener(OnHeadClicked);
        _bodyButton.onClick.AddListener(OnBodyClicked);
        _legButton.onClick.AddListener(OnLegClicked);
        _weaponsButton.onClick.AddListener(OnWeaponsClicked);
        _playerWeapons.onClick.AddListener(OnPlayerWeaponsClicked);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnBackButtonClicked);
        _headButton.onClick.RemoveListener(OnHeadClicked);
        _bodyButton.onClick.RemoveListener(OnBodyClicked);
        _legButton.onClick.RemoveListener(OnLegClicked);
        _weaponsButton.onClick.RemoveListener(OnWeaponsClicked);
        _playerWeapons.onClick.RemoveListener(OnPlayerWeaponsClicked);
    }

    private void OnBackButtonClicked()
    {
        _inventoryObserver.ClearView();
        _scrollView.SetActive(false);
        _cameraController.ResetPosition();
    }

    private void ShowItems(DetailType detailType)
    {
        _scrollView.SetActive(true);
        _playerWeapons.gameObject.SetActive(false);
        _cameraController.Translate(detailType);
        _inventoryObserver.ShowItems(detailType);
    }

    private void OnHeadClicked()
    {
        ShowItems(DetailType.Head);
    }

    private void OnBodyClicked()
    {
        ShowItems(DetailType.Body);
    }

    private void OnLegClicked()
    {
        ShowItems(DetailType.Leg);
    }

    private void OnWeaponsClicked()
    {
        ShowItems(DetailType.Weapons);
        _playerWeapons.gameObject.SetActive(true);
    }

    private void OnPlayerWeaponsClicked()
    {
        _weaponManager.gameObject.SetActive(true);
    }
}
