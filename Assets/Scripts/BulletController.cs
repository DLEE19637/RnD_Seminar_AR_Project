using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private BulletData BulletData;

    [SerializeField]
    private Transform EnemyTarget;

    private int damage;
    private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = BulletData.Speed;
        damage = BulletData.Damage;
    }
    public void SetTarget(Transform enemyTarget)
    {
        EnemyTarget = enemyTarget;    
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyTarget == null)
        {
            Destroy(gameObject);
            return;
        }


        Vector3 direction = EnemyTarget.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(EnemyTarget);
    }

    void HitTarget()
    {
        Damage(EnemyTarget);
        Destroy(gameObject);
    }
    void Damage(Transform enemy)
    {
        EnemyController enemeyController = enemy.GetComponent<EnemyController>();

        if (enemeyController != null)
        {
            enemeyController.TakeDamage(damage);
        }
    }

}
