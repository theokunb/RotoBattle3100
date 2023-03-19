using System;

[Serializable]
public class Experience
{
    private const int BaseValue = 500;

    private int _level;
    private int _currentValue;

    public event Action LevelUp;
    
    public Experience(int level, int value)
    {
        _level = level;
        _currentValue = value;
    }

    public Experience() { }

    public int Level => _level;
    public int CurrentValue => _currentValue;
    public int MaxValue => _level * BaseValue;

    public void AddExp(Enemy enemy)
    {
        _currentValue += 200;

        AddLevel(_currentValue / MaxValue);
    }

    private void AddLevel(int levels)
    {
        if(levels > 0)
        {
            _currentValue = _currentValue - MaxValue;
            _level++;
            LevelUp?.Invoke();

            AddLevel(_currentValue / MaxValue);
        }
    }
}
