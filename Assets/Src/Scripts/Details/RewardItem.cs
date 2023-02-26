using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class RewardItem : RewardView
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _title;

    private Detail _detail;

    public string DetailTitle => _detail?.Title;

    public void Render(Detail detail)
    {
        _detail = detail;
        _image.sprite = detail.GetComponent<DetailShop>().Icon;

        _title.GetComponent<LocalizeStringEvent>().RefreshString();
    }
}
