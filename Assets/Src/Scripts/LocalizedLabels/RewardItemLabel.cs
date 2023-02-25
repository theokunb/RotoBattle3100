using UnityEngine;

public class RewardItemLabel : MonoBehaviour
{
    [SerializeField] private RewardItem _rewardItem;

    public string Title => _rewardItem.DetailTitle;
}
