using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        Time.timeScale = 0;
    }

    protected virtual void OnDisable()
    {
        Time.timeScale = 1;
    }

    public abstract void Activated();
}
