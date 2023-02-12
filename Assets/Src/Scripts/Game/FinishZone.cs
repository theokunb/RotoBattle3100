using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField] Finish _finish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _finish.CloseDoor();
        }
    }
}
