using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameLeaderboard
{
    public static ILeaderboardRecorder Leaderboard { get; private set; } = new MyLeaderboard();
}


public class MyLeaderboard : ILeaderboardRecorder
{
    public void Record(PlayerData data)
    {

    }
}

public class YandexLeaderboard : ILeaderboardRecorder
{
    public void Record(PlayerData data)
    {
        Leaderboard.GetPlayerEntry(LeaderboardTables.BestPlayers, (response) =>
        {
            if (response == null || response.score < data.PlayerLevel)
            {
                Leaderboard.SetScore(LeaderboardTables.BestPlayers, data.PlayerLevel);
            }
        });
    }
}


public interface ILeaderboardRecorder
{
    void Record(PlayerData data);
}