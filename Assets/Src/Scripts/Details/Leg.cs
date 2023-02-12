using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Leg : RobotDetail
{
    private const string Label = "ноги";
    private const string MoveSpeed = "скорость передвижения:";
    private const float AnimationPlaybackFactor = 1f / 12;

    [SerializeField] private float _speed;

    private Animator _animator;
    private Character _character;

    public float Speed => _speed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _character = GetComponentInParent<Character>();
    }

    private void OnEnable()
    {
        _character.Moving.AddListener(OnMoving);
    }

    private void OnDisable()
    {
        _character.Moving.RemoveListener(OnMoving);
    }

    public override string GetLabel()
    {
        return Label;
    }

    public override string GetSpecialStats()
    {
        return $"{MoveSpeed} {Speed}";
    }

    private void OnMoving(float speed)
    {
        float value = Convert.ToInt32(speed > 0) * (_speed * AnimationPlaybackFactor);

        _animator.SetFloat(CharacterAnimationController.Param.Speed, value);
        
    }
}
