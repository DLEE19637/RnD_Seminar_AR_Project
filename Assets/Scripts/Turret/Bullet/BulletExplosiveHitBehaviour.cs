using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BulletExplosiveHitBehaviour : BulletHitBehaviourBase
{
    private readonly List<EnemyController> _enemiesInRange = new();

    public override void Trigger(Transform target)
    {
        foreach (var enemy in _enemiesInRange)
        {
            enemy.Damage(Damage);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyController>(out var enemyController))
        {
            _enemiesInRange.Add(enemyController);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<EnemyController>(out var enemyController))
        {
            _enemiesInRange.Remove(enemyController);
        }
    }
}
