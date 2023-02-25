using UnityEngine;

public class DetailInfoLabel : MonoBehaviour
{
    [SerializeField] private BuyWindow _buyWindow;

    public string Title => _buyWindow.ItemTitle;
    public string Description => _buyWindow.ItemDescription;
    public string Stats => _buyWindow.ItemStats;
}
