using UnityEngine;

public class DetailStatus : MonoBehaviour
{
    [SerializeField] private bool _isAvailable;
    [SerializeField] private bool _inShop;

    public bool IsAvailable => _isAvailable;
    public bool CanBuyInShop => _inShop;

    public void Unlock()
    {
        _isAvailable = true;
    }
}
