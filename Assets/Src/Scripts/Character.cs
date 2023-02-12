using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerScanner))]
[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    [SerializeField] private Transform _legPosition;

    private Leg _leg;
    private Body _body;
    private Head _head;

    protected PlayerScanner Scanner;
    protected List<Weapon> Weapons = new List<Weapon>();
    protected Leg Leg => _leg;
    protected Body Body => _body;
    protected Head Head => _head;

    public UnityEvent<float> Moving;

    public Transform LegPosition => _legPosition;
    public IEnumerable<WeaponPlace> WeaponPlaces => _body.WeaponPlaces;

    private void Update()
    {
        var target = Scanner.GetNearestEnemy();

        if (target != null)
        {
            _body.transform.LookAt(target.transform);
            _head.transform.LookAt(target.transform);
            _body.Attack(target);
        }
    }

    public void CorrectDetails(Transform legPosition)
    {
        _leg.SetPosition(legPosition);
        _body.SetPosition(_leg.UpperPlaceOfDetail);
        _head.SetPosition(_body.UpperPlaceOfDetail);
        _body.TryAddWeapons(Weapons);
    }

    public IEnumerable<DetailData> GetAllDetails()
    {
        var details = GetComponentsInChildren<Detail>();

        foreach (var detail in details)
        {
            yield return new DetailData(detail.Title);
        }
    }

    public void SetDetail(Leg leg)
    {
        Destroy(_leg?.gameObject);
        _leg = Instantiate(leg, transform);
    }

    public void SetDetail(Body body)
    {
        Destroy(_body?.gameObject);
        _body = Instantiate(body, transform);
    }

    public void SetDetail(Head head)
    {
        Destroy(_head?.gameObject);
        Scanner = GetComponent<PlayerScanner>();

        _head = Instantiate(head, transform);
        Scanner.InitializeHead(_head);
    }

    public void SetDetail(Weapon weapon)
    {
        Weapons.Insert(0, weapon);
    }

    public virtual int CalculateHealth()
    {
        return Leg.BonusHealth + Body.BonusHealth + Head.BonusHealth;
    }

    public virtual void SetWhoAttacked(Character character)
    {
    }
}