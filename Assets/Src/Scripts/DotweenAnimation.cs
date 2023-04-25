using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotweenAnimation : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _scaleValue;
    [SerializeField] private Vector3 _rotationValue;

    public TweenerCore<Vector3,Vector3, VectorOptions> ScaleAnimation()
    {
        return transform.DOScale(_scaleValue, _animationDuration);
    }

    public void Rotate()
    {
        transform.Rotate(_rotationValue * Time.deltaTime);
    }
}
