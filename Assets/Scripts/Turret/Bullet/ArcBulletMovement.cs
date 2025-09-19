using UnityEngine;

public class ArcBulletMovement : BulletMovement
{
    public override bool NeedsTarget => false;
    private Vector3 _targetPosition;

    [SerializeField]
    private float _gravity = 9.81f;

    private Vector3 _launchPosition;
    private float _travelTime;
    private float _timer;
    private float _targetDistance;
    private float _verticalVelocity;

    public override void SetTarget(Transform target)
    {
        base.SetTarget(target);
        _launchPosition = transform.position;
        _targetPosition = target.position;
        _targetDistance = (_targetPosition - transform.position).magnitude;
        _travelTime = _targetDistance / Velocity;
        _timer = 0.0f;
        _verticalVelocity = CalculateVerticalVelocity();
    }

    private float CalculateVerticalVelocity()
    {
        return (_targetPosition.y - _launchPosition.y) / _travelTime + (_gravity * _travelTime) / 2;
    }

    public override void Move()
    {
        _timer += Time.deltaTime;

        float x = Mathf.Lerp(_launchPosition.x, _targetPosition.x, _timer / _travelTime);
        float z = Mathf.Lerp(_launchPosition.z, _targetPosition.z, _timer / _travelTime);
        float y = _launchPosition.y + _verticalVelocity * _timer - (_gravity * Mathf.Pow(_timer, 2f)) / 2;

        transform.position = new Vector3(x, y, z);
    }

    public override bool HasReachedTarget()
    {
        return _timer >= _travelTime;
    }
}
