using UnityEngine;

public class WayPoint : MonoBehaviour
{
    void Start()
    {
		transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
		GameManager.Instance.RegisterWayPoint(this);
	}
}
