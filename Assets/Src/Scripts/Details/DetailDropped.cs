using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DetailDropped : MonoBehaviour
{
    private Detail _detail;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.GetComponent<Bag>().Put(this);
            gameObject.SetActive(false);
        }
    }

    public Detail GetDetail() => _detail;

    public void Initialize(Detail detail)
    {
        _detail = detail;
    }
}
