using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private TurretData TurretData;
    public Transform Target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToENemy = Vector3.Distance(transform.position, enemy.transform.position); 
            if (distanceToENemy < shortestDistance)
            {
                shortestDistance = distanceToENemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && shortestDistance <= TurretData.Range)
        {
            Target = closestEnemy.transform;
        }
        else
        {
            Target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return; // No target to shoot at
        }
    }

    void Shoot()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TurretData.Range); // Example radius
    }
}
