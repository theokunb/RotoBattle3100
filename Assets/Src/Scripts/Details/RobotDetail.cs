using UnityEngine;
using UnityEngine.Localization;

public abstract class RobotDetail : Detail
{
    [SerializeField] private Transform _upperPlaceOfDetail;
    [SerializeField] private int _bonusHealth;
    [SerializeField] private LocalizedString _bonusHealthDescription;

    public int BonusHealth => _bonusHealth;

    public Transform UpperPlaceOfDetail => _upperPlaceOfDetail;

    public override string GetStats()
    {
        return $"{_bonusHealthDescription.GetLocalizedString()}: {_bonusHealth}\n{GetSpecialStats()}";
    }

    public abstract string GetSpecialStats();
}
