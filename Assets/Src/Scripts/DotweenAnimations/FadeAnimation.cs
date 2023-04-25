using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : DotweenAnimation
{
    [SerializeField] private float _targetValue;
    [SerializeField] private int _loopCount;
    [SerializeField] private LoopType _loopType;

    public override void Animate()
    {
        if (TryGetComponent(out Image image))
        {
            image.DOFade(_targetValue, Duration)
                .SetLoops(_loopCount, _loopType)
                .SetUpdate(IsUnscaledTime);
        }
    }
}
