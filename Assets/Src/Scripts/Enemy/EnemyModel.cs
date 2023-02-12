using System;
using UnityEngine;

[Serializable]
public class EnemyModel
{
    [SerializeField] private int _id;
    [SerializeField] private Enemy _template;

    public int Id => _id;
    public Enemy Template => _template;
}
