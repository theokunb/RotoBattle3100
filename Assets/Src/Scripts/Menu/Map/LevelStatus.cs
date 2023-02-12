public class LevelStatus
{
    public LevelStatus(bool isCompleted, int todayCompletedTimes)
    {
        IsCompleted = isCompleted;
        TodayCompletedTimes = todayCompletedTimes;
    }

    public bool IsCompleted { get; private set; }
    public int TodayCompletedTimes { get; private set; }
}
