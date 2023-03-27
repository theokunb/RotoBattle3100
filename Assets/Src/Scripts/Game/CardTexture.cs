using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CardTexture : MonoBehaviour
{
    private Camera _camera;
    private Card _card;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _card = GetComponentInParent<Card>();

        if(_card != null)
        {
            _camera.targetTexture = _card.Texture;
        }
    }
}
