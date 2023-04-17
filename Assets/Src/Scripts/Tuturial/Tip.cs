using UnityEngine;
using UnityEngine.Localization;

public class Tip : MonoBehaviour
{
    [SerializeField] private LocalizedString _tip;

    public string GetTip() => _tip.GetLocalizedString();
}
