using UnityEngine;

public class RotationAnimation : DotweenAnimation
{
    [SerializeField] private Vector3 _rotationValue;

    public override void Animate()
    {
        transform.Rotate(_rotationValue * Time.deltaTime);
    }
}
