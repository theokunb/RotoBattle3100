using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Reward))]
public class Enemy : Character
{
    [SerializeField] private Leg _newLeg;
    [SerializeField] private Body _newBody;
    [SerializeField] private Head _newHead;
    [SerializeField] private List<Weapon> _newWeapons;

    private Character _target;
    private NavMeshAgent _agent;
    private Character _detectedTarget;

    public event Action<Character> EnemyDetected;
    
    public Character Target
    {
        get => _target;
        set
        {
            if(value == _target)
            {
                return;
            }

            _target = value;
        }
    }

    protected void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetDetail(_newLeg);
        SetDetail(_newBody);
        SetDetail(_newHead);
        InitializeWeapons(_newWeapons);
        CorrectDetails();
    }

    private void FixedUpdate()
    {
        StartFreeBie();
        DetectEnemy();

        Follow(Target);
    }

    public Enemy Create(Vector2 position)
    {
        var collider = GetComponent<BoxCollider>();
        Vector3 spawnPosition = new Vector3(position.x, collider.size.y / 2, position.y);

        return Instantiate(this, spawnPosition, Quaternion.identity);
    }

    public override void SetWhoAttacked(Character character)
    {
        EnemyDetected?.Invoke(character);
    }

    public override int CalculateHealth()
    {
        return base.CalculateHealth() / 2;
    }

    public override void SuspendMovement()
    {
        _agent.isStopped = true;
        Armory.Leg.Suspend();
    }

    public override void ResumeMovement()
    {
        _agent.isStopped = false;
    }

    private void InitializeWeapons(IEnumerable<Weapon> weapons)
    {
        foreach(var weapon in weapons)
        {
            SetDetail(weapon);
        }
    }

    private void DetectEnemy()
    {
        _detectedTarget = Armory.Scanner.GetNearestEnemy();

        if (_detectedTarget != null)
        {
            StopFreeBie();

            Armory.Body.transform.LookAt(_detectedTarget.transform);
            Armory.Body.Attack(_detectedTarget);
            EnemyDetected?.Invoke(_detectedTarget);
        }
    }

    private void Follow(Character character)
    {
        if(character != null)
        {
            Armory.Body.transform.LookAt(character.transform);
            _agent.SetDestination(character.transform.position);
            Moving?.Invoke(_agent.velocity.sqrMagnitude);
        }
    }
}
