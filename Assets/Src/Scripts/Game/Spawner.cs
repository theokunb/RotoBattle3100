using System;
using UnityEngine;

[RequireComponent(typeof(EnemyCounter))]
public class Spawner : MonoBehaviour
{
    private const int AllSpace = 100;
    private const int PlayerSpawn = 1;
    private const int StartFreeSpace = 30;
    private const int EndFreeSpace = 12;

    [SerializeField] private Player _player;
    [SerializeField] private LevelCreator _levelCreator;

    private Pack[] _packs;

    public event Action<int> EnemyAdded;
    public event Action EnemyDied;
    public event Action PlayerDied;

    private void OnEnable()
    {
        _player.GetComponent<Health>().Die += OnPlayerDied;
        GetComponent<EnemyCounter>().LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        GetComponent<EnemyCounter>().LevelCompleted -= OnLevelCompleted;

        foreach (var pack in _packs)
        {
            pack.UnsubscribeOnDetection();
            pack.EnemyDied -= OnEnemyDied;
        }
    }

    public void CreateEnemyPacks(Pack[] packs, Rectangle platform)
    {
        _packs = packs;
        float startPoint = platform.Lenght * StartFreeSpace / AllSpace;
        float endPoint = platform.Lenght * (AllSpace - EndFreeSpace) / AllSpace;
        var spaceBetweenPacks = (endPoint - startPoint) / packs.Length;

        for (int i = 0; i < packs.Length; i++)
        {
            packs[i].Create(startPoint + spaceBetweenPacks * i, platform.Width);
            packs[i].SubscribeOnDetection();
            packs[i].EnemyDied += OnEnemyDied;
            EnemyAdded?.Invoke(packs[i].GetCount());
        }
    }

    public void PutToStartPosition(Player player, Rectangle platform)
    {
        var position = new Vector3(platform.Width / 2, PlayerSpawn, PlayerSpawn);
        player.transform.position = position;
    }

    private void OnEnemyDied(Character character)
    {
        if (character.TryGetComponent(out Reward reward))
        {
            var randomReward = reward.GetReward();

            if (randomReward != null)
            {
                randomReward.transform.position = new Vector3(character.transform.position.x,
                    character.transform.GetComponent<BoxCollider>().size.y,
                    character.transform.position.z);
            }
        }

        var enemy = character as Enemy;

        if (enemy != null)
        {
            _player.AddExperience(enemy);
        }

        EnemyDied?.Invoke();
    }

    private void OnPlayerDied(Character character)
    {
        _player.GetComponent<Health>().Die -= OnPlayerDied;
        PlayerDied?.Invoke();
    }

    private void OnLevelCompleted()
    {
        _levelCreator.Finish.OpenDoor();
        _levelCreator.TerrainController.VisionCircleFadeOut();
    }
}
