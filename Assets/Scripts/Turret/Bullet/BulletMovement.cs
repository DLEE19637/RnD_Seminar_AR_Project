using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    protected float _hitRange = 0.01f;

    public float Velocity = 0.05f;
    protected Transform _target;

    public virtual bool NeedsTarget => true;

    public virtual void SetTarget(Transform target)
    {
        _target = target;
    }

    public virtual void Move()
    {
        transform.Translate(Time.deltaTime * Velocity * (_target.position - transform.position).normalized, Space.World);
    }

    public virtual bool HasReachedTarget()
    {
        return (_target.position - transform.position).magnitude <= _hitRange;
    }
}
