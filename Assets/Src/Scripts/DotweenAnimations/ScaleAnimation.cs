using DG.Tweening;
using System;
using UnityEngine;

public class ScaleAnimation : DotweenAnimation
{
    [SerializeField] private float _scaleValue;
    [SerializeField] private int _loopCount;
    [SerializeField] private LoopType _loopType;

    public override void Animate(Action onComplete = null)
    {
        transform.DOScale(_scaleValue, Duration)
            .SetLoops(_loopCount, LoopType.Yoyo)
            .SetUpdate(IsUnscaledTime)
            .OnComplete(() => onComplete?.Invoke());
    }
}
