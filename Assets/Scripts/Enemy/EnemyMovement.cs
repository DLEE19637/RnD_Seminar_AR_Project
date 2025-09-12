using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    private int _waypointIndex = 0;

    [SerializeField]
    private float _waypointReachedDistance = 0.1f;
    private float _waypointReachedDistanceSqr = 0.01f;
    private bool _reachedEnd = false;

    public UnityEvent ReachedEnd = new();

    private List<Transform> wayPointTransforms = new List<Transform>();

    private void Awake()
    {
        _waypointReachedDistanceSqr = _waypointReachedDistance * _waypointReachedDistance;
		
	}

	private void Start()
	{
		wayPointTransforms.Add(GameManager.Instance.EnemySpawn.transform);
		wayPointTransforms.AddRange(GameManager.Instance.WayPoints.Select(wp => wp.transform));
		wayPointTransforms.Add(GameManager.Instance.Base.transform);
	}

	public void Move(float speed)
    {
        if (_reachedEnd)
        {
            return;
        }

        Vector3 direction = (wayPointTransforms[_waypointIndex + 1].position -
			wayPointTransforms[_waypointIndex].position).normalized;

        transform.Translate(direction * (speed * Time.deltaTime));

        if (ReachedWayPoint())
        {
            ++_waypointIndex;
            if (_waypointIndex >=wayPointTransforms.Count - 1)
            {
                _reachedEnd = true;
                Debug.Log("End reached!");
                ReachedEnd.Invoke();
            }
        }
    }

    private bool ReachedWayPoint()
    {
        float distance = (transform.position - wayPointTransforms[_waypointIndex + 1].position).sqrMagnitude;

        return distance < _waypointReachedDistanceSqr;
    }
}
