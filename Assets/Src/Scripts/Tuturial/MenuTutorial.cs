using UnityEngine;

public class MenuTutorial : Tutorial
{
    [SerializeField] private TutorialButton _button;

    public override void Completed()
    {
        Player.IsMenuTutorialCompleted = true;
        _button.gameObject.SetActive(true);
    }

    public override bool IsTutorialCompleted()
    {
        return Player.IsMenuTutorialCompleted;
    }
}
