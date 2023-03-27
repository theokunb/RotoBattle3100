using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public abstract class CardText : MonoBehaviour
{
    protected TMP_Text Text;
    protected Card Card;

    private void Start()
    {
        Text = GetComponent<TMP_Text>();
        Card = GetComponentInParent<Card>();

        if (Card != null)
        {
            SetText();
        }
    }

    protected abstract void SetText();
}
