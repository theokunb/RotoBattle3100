using System;

[Serializable]
public class PlayerProgress
{
    private int _completedLevels;
    private DateTime _lastGamedDay;

    public PlayerProgress()
    {
        _completedLevels = 0;
        _lastGamedDay = DateTime.Now;
    }

    public PlayerProgress(int completedLevels, DateTime lastGamedDay)
    {
        _completedLevels = completedLevels;
        _lastGamedDay = lastGamedDay;
    }

    public int GetCompletedLevels() => _completedLevels;

    public DateTime GetLastGamedDay() => _lastGamedDay;



    public LevelStatus GetStatus(int levelId)
    {
        return new LevelStatus(_completedLevels >= levelId);
    }
    public void PlayedLevel(int idLevel)
    {
        AddProgress(idLevel);
        VisitGame();
    }

    public void VisitGame()
    {
        if ((DateTime.Now - _lastGamedDay.Date).Days >= 1)
        {
            _lastGamedDay = DateTime.Now;
        }
    }

    private void AddProgress(int completedLevel)
    {
        completedLevel++;

        if (_completedLevels < completedLevel)
        {
            _completedLevels = completedLevel;
        }
    }
}
