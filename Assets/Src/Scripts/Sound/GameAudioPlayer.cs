using Unity.VisualScripting;
using UnityEngine;

public class GameAudioPlayer : BackgroundAudioPlayer
{
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private LevelCreator _levelCreator;
    [SerializeField] private AudioClip _levelCompleted;
    [SerializeField] private AudioClip _celebration;

    private void OnEnable()
    {
        _enemyCounter.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        _enemyCounter.LevelCompleted -= OnLevelCompleted;
    }

    protected override void Start()
    {
        base.Start();
        _levelCreator.Finish.LevelEnded += OnFinish;
    }

    private void OnLevelCompleted()
    {
        var audioSource = Sounds.GetAudioSource();

        audioSource?.PlayOneShot(_levelCompleted);
    }

    private void OnFinish(Finish finish)
    {
        finish.LevelEnded -= OnFinish;
        var audioSource = Sounds.GetAudioSource();
        audioSource.PlayOneShot(_celebration);
    }
}
