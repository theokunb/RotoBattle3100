using UnityEngine;

public class GameTutorial : Tutorial
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerScanner _scanner;
    [SerializeField] private StickController _stickController;
 
    public override void Completed()
    {
        _mover.enabled = true;
        _scanner.enabled = true;
        _stickController.enabled = true;
        Player.IsGameTutorialCompleted = true;
    }

    public override bool IsTutorialCompleted()
    {
        return Player.IsGameTutorialCompleted;
    }

    protected override void BeginTutorial()
    {
        base.BeginTutorial();
        _mover.enabled = false;
        _scanner.enabled = false;
        _stickController.enabled = false;
    }
}
