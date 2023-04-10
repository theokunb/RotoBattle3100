using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Level/New Level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private GameObject _boundTemplate;
    [SerializeField] private Finish _finishTemplate;
    [SerializeField] private LocalizedString _title;
    [SerializeField] private int _width;
    [SerializeField] private int _lenght;
    [SerializeField] private Pack[] _enemies;
    [SerializeField] private int _rewardCountInDay;
    [SerializeField] private Sprite _icon;
    [SerializeField] private List<Price> _reward;

    public int Id => _id;
    public Terrain Terrain => _terrain;
    public GameObject Bound => _boundTemplate;
    public Finish Finish => _finishTemplate;
    public string Title => _title.GetLocalizedString();
    public Pack[] Enemies => _enemies;
    public int Width => _width;
    public int Lenght => _lenght;
    public Sprite Icon => _icon;
    public IEnumerable<Currency> Reward => _reward.Select(element => element.ToCurrency());
}