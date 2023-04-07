public class LevelStatus
{
    public LevelStatus(bool isCompleted)
    {
        IsCompleted = isCompleted;
    }

    public bool IsCompleted { get; private set; }
}
