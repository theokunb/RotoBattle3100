using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class RobotDetail : Detail
{
    protected const string BonusHealthDescription = "дополнительное здоровье:";

    [SerializeField] private Transform _upperPlaceOfDetail;
    [SerializeField] private int _bonusHealth;

    public int BonusHealth => _bonusHealth;

    public Transform UpperPlaceOfDetail => _upperPlaceOfDetail;

    public override string GetStats()
    {
        return $"{BonusHealthDescription} {_bonusHealth}\n{GetSpecialStats()}";
    }

    public abstract string GetSpecialStats();
}
