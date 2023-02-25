using System;
using UnityEngine;
using UnityEngine.Localization;

[RequireComponent(typeof(Animator))]
public class Leg : RobotDetail
{
    private const float AnimationPlaybackFactor = 1f / 45;

    [SerializeField] private float _speed;
    [SerializeField] private LocalizedString _moveSteed;

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

    public override string GetSpecialStats()
    {
        return $"{_moveSteed.GetLocalizedString()}: {Speed}";
    }

    public void Suspend()
    {
        _animator.SetFloat(CharacterAnimationController.Param.Speed, 0);
    }

    private void OnMoving(float speed)
    {
        float value = Convert.ToInt32(speed > 0) * (_speed * AnimationPlaybackFactor);

        _animator.SetFloat(CharacterAnimationController.Param.Speed, value);
    }
}
