using DG.Tweening;
using UnityEngine;

public class DetailDropped : MonoBehaviour
{
    [SerializeField] private DotweenAnimation _scaleAnimation;
    [SerializeField] private DotweenAnimation _rotationAnimation;

    private Detail _detail;

    private void Start()
    {
        _scaleAnimation.Animate();
        _rotationAnimation.Animate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bag bag))
        {
            bag.Put(this);
            gameObject.SetActive(false);
        }
    }

    public Detail GetDetail() => _detail;

    public void Initialize(Detail detail)
    {
        _detail = detail;
    }

    public void Unlock()
    {
        _detail?.Unlock();
    }
}
