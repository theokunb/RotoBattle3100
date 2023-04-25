using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(DotweenAnimation))]
public class DetailDropped : MonoBehaviour
{
    private Detail _detail;
    private DotweenAnimation _animation;

    private void Start()
    {
        _animation = GetComponent<DotweenAnimation>();

        _animation.ScaleAnimation()
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bag bag))
        {
            bag.Put(this);
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        _animation.Rotate();
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
