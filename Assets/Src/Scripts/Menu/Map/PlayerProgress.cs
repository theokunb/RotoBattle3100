using System;

[Serializable]
public class PlayerProgress
{
    private int _completedLevels;
    
    public PlayerProgress()
    {
        _completedLevels = 0;
        LastGamedDay = DateTime.Now;
    }

    public PlayerProgress(int completedLevels, DateTime lastGamedDay)
    {
        _completedLevels = completedLevels;
        LastGamedDay = lastGamedDay;
    }

    public DateTime LastGamedDay { get; set; }
    public int GetCompletedLevels() => _completedLevels;

    public LevelStatus GetStatus(int levelId)
    {
        return new LevelStatus(_completedLevels >= levelId);
    }
    public void PlayedLevel(int idLevel)
    {
        AddProgress(idLevel);
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
