using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivator : MonoBehaviour
{
    [SerializeField] private List<ChangabeButton> _buttons;

    private void OnEnable()
    {
        foreach(var button in _buttons)
        {
            button.Performed+= OnButtonPerformed;
        }
    }

    private void OnDisable()
    {
        foreach(var button in _buttons)
        {
            button.Performed -= OnButtonPerformed;
        }
    }

    private void OnButtonPerformed(ChangabeButton performedButton)
    {
        foreach(var button in _buttons)
        {
            if(button != performedButton)
            {
                button.SetInactive();
            }
            else
            {
                button.SetActive();
            }
        }
    }

    public void ResetAll()
    {
        OnButtonPerformed(null);
    }
}
