using UnityEngine;

public class BoxDetail : MonoBehaviour
{
    [SerializeField] private DetailDropped _template;

    public DetailDropped Boxing()
    {
        if(TryGetComponent(out Detail detail))
        {
            var createdDetail = Instantiate(_template);
            createdDetail.Initialize(detail);
            return createdDetail;
        }
        else
        {
            return null;
        }
    }
}
