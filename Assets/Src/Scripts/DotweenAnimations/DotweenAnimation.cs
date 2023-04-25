using System;
using UnityEngine;

public abstract class DotweenAnimation : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private bool _isUnscaledTime;

    protected float Duration => _animationDuration;
    protected bool IsUnscaledTime => _isUnscaledTime;

    public abstract void Animate(Action onCompleteCallback = null);
}