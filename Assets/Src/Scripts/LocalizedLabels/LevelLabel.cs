using UnityEngine;

public class LevelLabel : MonoBehaviour
{
    [SerializeField] private LevelView _levelView;

    public LevelView LevelView => _levelView;
}
