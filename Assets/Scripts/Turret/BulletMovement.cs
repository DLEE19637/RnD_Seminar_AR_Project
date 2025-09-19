using UnityEngine;

public abstract class BulletMovement : MonoBehaviour
{
    public abstract void SetTarget(Transform target);

    public abstract void Move();
}
