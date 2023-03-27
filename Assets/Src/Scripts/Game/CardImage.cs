using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class CardImage : MonoBehaviour
{
    protected Image Image;
    protected Card Card;

    private void Start()
    {
        Image = GetComponent<Image>();

        Card = GetComponentInParent<Card>();

        if (Card != null)
        {
            SetImage();
        }
    }

    protected abstract void SetImage();
}