using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Details", menuName = "ItemsPull", order = 51)]
public class ItemsPull : ScriptableObject
{
    [SerializeField] private List<Detail> _details;

    public IEnumerable<Detail> Details => _details;

    public DetailDropped GetRandomDetail()
    {
        int index = Random.Range(0, _details.Count);
        DetailDropped detailDropped = _details[index].GetComponent<BoxDetail>().Boxing();
        detailDropped.Initialize(_details[index]);
        return detailDropped;
    }
}
