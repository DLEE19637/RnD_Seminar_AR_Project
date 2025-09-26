using UnityEngine;

public abstract class BulletHitBehaviourBase : MonoBehaviour
{
    public int Damage { get; set; }

    public abstract void Trigger(Transform target);
}
