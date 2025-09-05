using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private BulletData BulletData;

    [SerializeField]
    private Transform EnemyTarget;

    private int damage;
    private float speed;

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
        if (EnemyTarget == null && BulletData.Type != BulletType.Piercing)
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
        if (BulletData.Type != BulletType.Piercing)
        {
            Destroy(gameObject);
        }
    }
    void Damage(Transform enemy)
    {
        if (enemy.TryGetComponent<EnemyController>(out var enemyController))
        {
            enemyController.Damage(damage);
        }
    }
}
