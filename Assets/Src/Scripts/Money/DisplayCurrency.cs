using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DisplayCurrency : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _value;

    public void Render(Sprite icon, Currency currency)
    {
        _image.sprite = icon;
        _value.text = currency.Count.ToString();
    }
}
