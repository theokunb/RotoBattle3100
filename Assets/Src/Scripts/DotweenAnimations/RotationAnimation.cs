using System;
using UnityEngine;

public class RotationAnimation : DotweenAnimation
{
    [SerializeField] private Vector3 _rotationValue;

    public override void Animate(Action onComplete)
    {
        transform.Rotate(_rotationValue * Time.deltaTime);
    }
}
