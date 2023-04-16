using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Localization;

public class LeaderboardPanel : MonoBehaviour
{
    private const int TopPlayersCount = 5;

    [SerializeField] private LocalizedString _unknownName;
    [SerializeField] private LeaderboardEntry _template;
    [SerializeField] private GameObject _errorTemplate;
    [SerializeField] private GameObject _separator;
    [SerializeField] private Transform _container;

    public void OnEnable()
    {
        ShowTop();
        ShowPlayer();
    }

    public void OnDisable()
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
    }

    private void ShowPlayer()
    {
        Leaderboard.GetPlayerEntry(LeaderboardTables.BestPlayers, (response) =>
        {
            if(response.rank > TopPlayersCount)
            {
                Instantiate(_separator, _container);
                var playerEntry = Instantiate(_template, _container);
                playerEntry.Render(response.rank, CheckName(response.player.publicName), response.score);
            }
        });
    }

    private void ShowTop()
    {
        Leaderboard.GetEntries(LeaderboardTables.BestPlayers, (response) =>
        {
            int place = 1;

            foreach (var entry in response.entries)
            {
                var leaderboardEntry = Instantiate(_template, _container);
                leaderboardEntry.Render(place++, CheckName(entry.player.publicName), entry.score);
            }
        },
        (error) =>
        {
            Instantiate(_errorTemplate, _container);
        },
        TopPlayersCount);
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
}
