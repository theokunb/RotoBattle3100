using Agava.YandexGames;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

[RequireComponent(typeof(UpgradesView))]
public class UpgradesObserver : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LocalizedString _unknownName;
    [SerializeField] private LocalizedString _level;
    [SerializeField] private LocalizedString _availableUpgrades;
    [SerializeField] private Button _backButton;

    private UpgradesView _view;

    private void Awake()
    {
        _view = GetComponent<UpgradesView>();
    }

    private void OnEnable()
    {
        _player.HealthUpgraded += OnHealthUpgraded;
        _player.ShieldUpgraded += OnShieldUpgraded;
        _player.SpeedUpgraded += OnSpeedUpgraded;
        _view.Reset += OnReset;
        _backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnDisable()
    {
        _player.HealthUpgraded -= OnHealthUpgraded;
        _player.ShieldUpgraded -= OnShieldUpgraded;
        _player.SpeedUpgraded -= OnSpeedUpgraded;
        _view.Reset -= OnReset;
        _backButton.onClick.RemoveListener(OnBackButtonClicked);
    }

    private void Start()
    {
        SetPlayerName();
        SetFreeUpgrades();

        OnHealthUpgraded();
        OnShieldUpgraded();
        OnSpeedUpgraded();
    }

    public void OnHeathClicked()
    {
        _player.AddUpgrade(Upgrades.Health);
    }

    public void OnShieldClicked()
    {
        _player.AddUpgrade(Upgrades.Shield);
    }

    public void OnSpeedClicked()
    {
        _player.AddUpgrade(Upgrades.Speed);
    }

    private void SetPlayerName()
    {
        Leaderboard.GetPlayerEntry(LeaderboardTables.BestPlayers, (response) =>
        {
            string name = CheckName(response.player.publicName);
            string level = _player.Experience.Level.ToString();

            _view.SetPlayerData($"{name} {level} {_level.GetLocalizedString()}");
        });
    }

    private void SetFreeUpgrades()
    {
        int maxPoints = _player.Experience.Level - 1;
        int wastedPoints = _player.Upgrade.GetUpgrades().Count();

        _view.SetAvailableUpgrades(_availableUpgrades.GetLocalizedString(), wastedPoints, maxPoints);
    }

    private string CheckName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return _unknownName.GetLocalizedString();
        }
        else
        {
            return name;
        }
    }

    private void OnSpeedUpgraded()
    {
        _view.SetSpeedCount(_player.Upgrade.GetUpgradesCount(Upgrades.Speed));
        SetFreeUpgrades();
    }

    private void OnShieldUpgraded()
    {
        _view.SetShieldCount(_player.Upgrade.GetUpgradesCount(Upgrades.Shield));
        SetFreeUpgrades();
    }

    private void OnHealthUpgraded()
    {
        _view.SetHealthCount(_player.Upgrade.GetUpgradesCount(Upgrades.Health));
        SetFreeUpgrades();
    }

    private void OnReset()
    {
        _player.ResetUpgrades();
    }

    private void OnBackButtonClicked()
    {
        _player.Save();
    }
}
