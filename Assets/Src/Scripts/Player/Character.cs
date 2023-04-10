using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerScanner))]
[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _freeBieSpeed;
    [SerializeField] private Transform _legPosition;

    private Coroutine _freebieTask;

    protected ArmoryVisitor Armory = new ArmoryVisitor();

    public UnityEvent<float> Moving;

    public Transform LegPosition => _legPosition;
    public IEnumerable<WeaponPlace> WeaponPlaces => Armory.Body.WeaponPlaces;

    private void FixedUpdate()
    {
        var target = Armory.Scanner?.GetNearestEnemy();

        StartFreeBie();

        if (target != null)
        {
            StopFreeBie();

            Armory.Body.transform.LookAt(target.transform);
            Armory.Head.transform.LookAt(target.transform);
            Armory.Body.Attack(target);
        }
    }

    protected void StartFreeBie()
    {
        if (_freebieTask == null)
        {
            _freebieTask = StartCoroutine(DoFreebie());
        }
    }

    protected void StopFreeBie()
    {
        if(_freebieTask != null)
        {
            StopCoroutine(_freebieTask);
            _freebieTask = null;
        }
    }

    private IEnumerator DoFreebie()
    {
        float angle = -20;
        float targetY = Armory.Body.transform.rotation.y;
        Health health = GetComponent<Health>();

        while (health.IsAlive)
        {
            targetY = Mathf.MoveTowards(targetY, angle, _freeBieSpeed * Time.deltaTime);
            Armory.Body.transform.localRotation = Quaternion.Euler(0, targetY, 0);
            Armory.Head.transform.localRotation = Quaternion.Euler(0, targetY, 0);

            yield return new WaitForFixedUpdate();
            if (targetY == angle)
            {
                angle *= -1;
            }
        }
    }

    public IEnumerable<long> GetAllDetails()
    {
        var details = GetComponentsInChildren<Detail>();

        foreach (var detail in details)
        {
            yield return detail.Id;
        }
    }

    public void SetDetail(Detail detail)
    {
        detail?.Accept(Armory, transform);
    }

    public void CorrectDetails()
    {
        Armory?.CorrectDetails(this);
    }

    public virtual int CalculateHealth()
    {
        return Armory.Leg.BonusHealth + Armory.Body.BonusHealth + Armory.Head.BonusHealth;
    }

    public virtual void SetWhoAttacked(Character character)
    {
    }

    public virtual void SuspendMovement() { }

    public virtual void ResumeMovement() { }

    public class ArmoryVisitor : IDetailCreator
    {
        private List<Weapon> _weapons;

        public Head Head { get; private set; }
        public Body Body { get; private set; }
        public Leg Leg { get; private set; }
        public PlayerScanner Scanner { get; private set; }
        public IEnumerable<Weapon> Weapons => _weapons;

        public ArmoryVisitor()
        {
            _weapons = new List<Weapon>();
        }

        public void CorrectDetails(Character parent)
        {
            Leg?.SetPosition(parent.LegPosition);
            Body?.SetPosition(Leg?.UpperPlaceOfDetail);
            Head?.SetPosition(Body?.UpperPlaceOfDetail);
            Body?.TryAddWeapons(_weapons);
        }

        public void Create(Head head, Transform parent)
        {
            if (Head != null)
            {
                Destroy(Head.gameObject);
            }

            Head = Instantiate(head, parent);
            Scanner = parent.GetComponent<PlayerScanner>();
            Scanner?.InitializeHead(Head);
        }

        public void Create(Body body, Transform parent)
        {
            if (Body != null)
            {
                Destroy(Body.gameObject);
            }

            Body = Instantiate(body, parent);
        }

        public void Create(Leg leg, Transform parent)
        {
            if (Leg != null)
            {
                Destroy(Leg.gameObject);
            }

            Leg = Instantiate(leg, parent);
        }

        public void Create(Weapon weapon, Transform parent)
        {
            _weapons.Insert(0, weapon);
        }
    }
}


public interface IDetailCreator
{
    void Create(Head head,Transform parent);
    void Create(Body body, Transform parent);
    void Create(Leg leg, Transform parent);
    void Create(Weapon weapon, Transform parent);
}