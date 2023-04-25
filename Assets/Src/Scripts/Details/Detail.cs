using UnityEngine;
using UnityEngine.Localization;

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
    public string Id => _title.TableEntryReference.KeyId.ToString();
    public LocalizedString Label => _label;

    public void SetPosition(Transform target)
    {
        transform.position = target.position;
        transform.parent = target.transform;
    }

    public void Unlock()
    {
        GetComponent<DetailStatus>().Unlock();
    }

    public abstract string GetStats();

    public abstract void Accept(IDetailCreator creator, Transform parent);
}
