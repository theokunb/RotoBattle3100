using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class DetailDropped : MonoBehaviour
{
    private Detail _detail;
    private Coroutine _animationTask;

    private void Start()
    {
        _animationTask = StartCoroutine(AnimationTask());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.GetComponent<Bag>().Put(this);
            StopCoroutine(_animationTask);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator AnimationTask()
    {
        transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);

        while (true)
        {
            transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
            yield return null;
        }
    }

    public Detail GetDetail() => _detail;

    public void Initialize(Detail detail)
    {
        _detail = detail;
    }
}
