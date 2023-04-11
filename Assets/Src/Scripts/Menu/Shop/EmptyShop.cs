using DG.Tweening;
using UnityEngine;

public class EmptyShop : MonoBehaviour
{
    [SerializeField] private float _scaleTime;

    private void OnEnable()
    {
        transform.localScale = new Vector3(0, 1, 1);
        transform.DOScaleX(1, _scaleTime);
    }
}
