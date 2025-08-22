using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    private int _waypointIndex = 0;

    [SerializeField]
    private float _waypointReachedDistance = 0.1f;
    private float _waypointReachedDistanceSqr = 0.01f;
    private bool _reachedEnd = false;

    private List<Transform> _wayPoints;
    public List<Transform> WayPoints
    {
        get => _wayPoints;
        set
        {
            _wayPoints = value;
            _waypointIndex = 0;
        }
    }

    public UnityEvent ReachedEnd = new();

    private void Awake()
    {
        _waypointReachedDistanceSqr = _waypointReachedDistance * _waypointReachedDistance;
    }

    public void Move(float speed)
    {
        if (_reachedEnd)
        {
            return;
        }

        Vector3 direction = (_wayPoints[_waypointIndex + 1].position - _wayPoints[_waypointIndex].position).normalized;

        transform.Translate(direction * (speed * Time.deltaTime));

        if (ReachedWayPoint())
        {
            ++_waypointIndex;
            if (_waypointIndex >= _wayPoints.Count - 1)
            {
                _reachedEnd = true;
                ReachedEnd.Invoke();
            }
        }
    }

    private bool ReachedWayPoint()
    {
        float distance = (transform.position - _wayPoints[_waypointIndex + 1].position).sqrMagnitude;

        return distance < _waypointReachedDistanceSqr;
    }
}
