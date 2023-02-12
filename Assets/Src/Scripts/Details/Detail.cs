using UnityEngine;

[RequireComponent(typeof(DetailShop))]
[RequireComponent(typeof(DetailStatus))]
[RequireComponent(typeof(BoxDetail))]
public abstract class Detail : MonoBehaviour
{
    [SerializeField] private string _title;
    [SerializeField] private string _description;

    public string Title => _title;
    public string Description => _description;
    public bool IsAvailable => GetComponent<DetailStatus>().IsAvailable;
    public bool CanBuyInShop => GetComponent<DetailStatus>().CanBuyInShop;

    public void SetPosition(Transform target)
    {
        transform.position = target.position;
    }

    public void Unlock()
    {
        GetComponent<DetailStatus>().Unlock();
    }

    public abstract string GetLabel();

    public abstract string GetStats();
}
