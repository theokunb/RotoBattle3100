using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text _rank;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private LocalizedString _levelKey;

    public void Render(int rank, string name, int score)
    {
        _rank.text = $"{rank}.";
        _name.text = name;
        _score.text = $"{score} {_levelKey.GetLocalizedString()}";
    }
}