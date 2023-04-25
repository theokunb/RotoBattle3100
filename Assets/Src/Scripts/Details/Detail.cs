using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

[RequireComponent(typeof(DetailShop))]
[RequireComponent(typeof(DetailStatus))]
[RequireComponent(typeof(BoxDetail))]
public abstract class Detail : MonoBehaviour
{
    [SerializeField] private LocalizedString _title;
    [SerializeField] private LocalizedString _description;
    [SerializeField] private LocalizedString _label;

    public string Title => _title.GetLocalizedString();
    public string Description => _description.GetLocalizedString();
    public bool IsAvailable => GetComponent<DetailStatus>().IsAvailable;
    public bool CanBuyInShop => GetComponent<DetailStatus>().CanBuyInShop;
    public string Id => _title.TableEntryReference.KeyId.ToString();

    public void SetPosition(Transform target)
    {
        transform.position = target.position;
        transform.parent = target.transform;
    }

    public void Unlock()
    {
        GetComponent<DetailStatus>().Unlock();
    }

    public LocalizedString Label => _label;

    public abstract string GetStats();
    public abstract void Accept(IDetailCreator creator, Transform parent);
}
