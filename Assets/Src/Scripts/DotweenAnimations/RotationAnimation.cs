using DG.Tweening;
using System;
using UnityEngine;

public class RotationAnimation : DotweenAnimation
{
    [SerializeField] private Vector3 _rotationValue;
    [SerializeField] private int _loopCount;

    public override void Animate(Action onComplete)
    {
        transform.DORotate(_rotationValue, Duration)
            .SetLoops(_loopCount, LoopType.Incremental)
            .SetUpdate(IsUnscaledTime);
    }
}
