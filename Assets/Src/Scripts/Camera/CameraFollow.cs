using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _offset;

    private float _zPosition;

    private void Start()
    {
        _zPosition = _player.transform.position.z - transform.position.z;
        StartCoroutine(Follow(_player));
    }

    private IEnumerator Follow(Player player)
    {
        while (true)
        {
            transform.position = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);

            float newZ = player.transform.position.z - transform.position.z;

            if (Mathf.Abs(newZ - _zPosition) > _offset)
            {
                Vector3 movement = new Vector3(0, 0, newZ - _zPosition);

                transform.position += movement * Time.deltaTime;
            }

            yield return null;
        }
    }
}
