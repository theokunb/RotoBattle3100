using IJunior.TypedScenes;
using UnityEngine;

public class Game : MonoBehaviour, ISceneLoadHandler<int>
{
    private const int HeightPlatform = 10;

    [SerializeField] private LevelCreator _levelCreator;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private PlayerLoader _playerLoader;
    [SerializeField] private Player _player;
    [SerializeField] private LevelsContainer _levelContainer;
    [SerializeField] private MenuBackground _menuBackground;
    [SerializeField] private FinishMenu _finishMenu;
    [SerializeField] private LoseMenu _loseMenu;

    private Level _currentLevel;

    public Level CurrentLevel => _currentLevel;

    private void OnEnable()
    {
        _spawner.PlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        _spawner.PlayerDied -= OnPlayerDied;
    }

    private void Start()
    {
        _currentLevel = _levelContainer.GetLevel(0);
        _levelCreator.Create(_currentLevel.Width, _currentLevel.Lenght, HeightPlatform);
        _spawner.CreateEnemyPacks(_currentLevel.Enemies, new Rectangle(_currentLevel.Width, _currentLevel.Lenght));
        _spawner.PutToStartPosition(_player, new Rectangle(_currentLevel.Width, _currentLevel.Lenght));

        _levelCreator.Finish.LevelEnded += OnLevelEnded;
    }

    public void OnSceneLoaded(int argument)
    {
        _currentLevel = _levelContainer.GetLevel(argument);
    }

    public bool HaveNextLevel()
    {
        return _levelContainer.GetLevel(_currentLevel.Id + 1) != null;
    }

    private void OnLevelEnded(Finish finish)
    {
        finish.LevelEnded -= OnLevelEnded;
        _playerLoader.PlayerProgress.PlayedLevel(_currentLevel.Id);

        Bag playerBag = _player.GetComponent<Bag>();

        _menuBackground.gameObject.SetActive(true);
        _finishMenu.SetRewards(playerBag);
        _menuBackground.OnepMenu(_finishMenu);
        Save();
    }

    private void Save()
    {
        GameStorage.Save(new PlayerData(_player), GameStorage.PlayerData);
        GameStorage.Save(_playerLoader.PlayerProgress, GameStorage.PlayerProgress);
    }

    private void OnPlayerDied()
    {
        _menuBackground.gameObject.SetActive(true);
        _menuBackground.OnepMenu(_loseMenu);
    }
}
