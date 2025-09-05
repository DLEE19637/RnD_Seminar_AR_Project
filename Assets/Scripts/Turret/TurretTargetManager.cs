using System.Collections.Generic;
using UnityEngine;

#nullable enable
[RequireComponent(typeof(Collider))]
public class TurretTargetManager : MonoBehaviour
{
    private const int TerrainLayerIndex = 6;

    public enum TargetingMode
    {
        Closest
    }

    private readonly List<EnemyController> _enemiesWithinTrigger = new();
    public TargetingMode CurrentTargetingMode = TargetingMode.Closest;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<EnemyController>(out var enemyController))
        {
            return;
        }

        _enemiesWithinTrigger.Add(enemyController);
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyController? enemyController = other.GetComponent<EnemyController>();
        if (enemyController == null)
        {
            return;
        }

        _enemiesWithinTrigger.Remove(enemyController);
    }

    public EnemyController? ProvideTarget()
    {
        if (_enemiesWithinTrigger.Count == 0)
        {
            return null;
        }

        switch (CurrentTargetingMode)
        {
            case TargetingMode.Closest:
                return GetClosestTarget();

            default:
                Debug.LogError($"{gameObject.name}.{nameof(ProvideTarget)}: {gameObject.name} doesn't have a valid targeting mode");
                return null;
        }
    }

    private EnemyController? GetClosestTarget()
    {
        EnemyController? target = null;
        float minDistance = float.MaxValue;
        foreach (EnemyController targetTransform in _enemiesWithinTrigger)
        {
            Vector3 direction = transform.position - targetTransform.transform.position;
            float distance = direction.magnitude;

            if (distance > minDistance)
            {
                continue;
            }

            direction /= distance;

            if (IsObscuredByTerrain(direction, distance))
            {
                continue;
            }

            minDistance = distance;
            target = targetTransform;
        }

        return target;
    }

    private bool IsObscuredByTerrain(Vector3 direction, float distance)
    {
        return Physics.Raycast(new Ray(transform.position, direction), distance, TerrainLayerIndex);
    }
}
