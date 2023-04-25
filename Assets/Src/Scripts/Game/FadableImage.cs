using System;
using UnityEngine;

public class FadableImage : MonoBehaviour
{
    [SerializeField] private DotweenAnimation _fadeInAnimation;
    [SerializeField] private DotweenAnimation _fadeOutAnimation;

    public void FadeIn(Action onComplete = null)
    {
        _fadeInAnimation.Animate(onComplete);
    }

    public void FadeOut(Action onComplete = null)
    {
        _fadeOutAnimation.Animate(onComplete);
    }
}
