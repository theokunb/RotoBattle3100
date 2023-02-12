using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/Level Container", order = 51)]
public class LevelsContainer : ScriptableObject
{
    [SerializeField] private Level[] _levels;

    public Level GetLevel(int id)
    {
        if (id < _levels.Length)
        {
            return _levels[id];
        }
        else
        {
            return null;
        }
    }

    public IEnumerable<Level> Levels => _levels;
}
