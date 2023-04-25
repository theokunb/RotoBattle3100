using UnityEngine;

public class FadableImage : MonoBehaviour
{
    [SerializeField] private DotweenAnimation _fadeInAnimation;
    [SerializeField] private DotweenAnimation _fadeOutAnimation;

    public void FadeIn()
    {
        _fadeInAnimation.Animate();
    }

    public void FadeOut()
    {
        _fadeOutAnimation.Animate();
    }
}
