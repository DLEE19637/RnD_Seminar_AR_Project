using UnityEngine;

public class NormalBulletHitBehaviour : BulletHitBehaviourBase
{
    public override void Trigger(Transform target)
    {
        if (target.TryGetComponent<EnemyController>(out var enemy))
        {
            enemy.Damage(Damage);
        }

        Destroy(gameObject);
    }
}
