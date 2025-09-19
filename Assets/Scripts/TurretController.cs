using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private TurretData TurretData;
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private Transform TurretRotation;
    [SerializeField]
    private Transform BulletSpawn;
    [SerializeField]
    private AudioClip TurretShootSound;

    public Transform Target;

    private float shootingCooldown = 0f;

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

    void Update()
    {
        if (Target == null)
        {
            return;
        }

        RotateToTarget();

        if (!IsTargetAligned())
        {
            return;
        }

        if(shootingCooldown <= 0f)
        {
            Shoot();
            shootingCooldown = 1f / TurretData.FireRate;
        }

        shootingCooldown -= Time.deltaTime;
    }

    void RotateToTarget()
    {
        Vector3 dir = Target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(TurretRotation.rotation, lookRotation, Time.deltaTime * TurretData.RotationSpeed).eulerAngles;
        TurretRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    bool IsTargetAligned()
    {
        Vector3 dirToTarget = (Target.position - TurretRotation.position).normalized;
        float angle = Vector3.Angle(TurretRotation.forward, dirToTarget);
        return angle < 20f;
    }

    void Shoot()
    {
        SoundManager.Instance.PlaySoundEffect(TurretShootSound);
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        if (bullet != null)
        {
            bulletController.SetTarget(Target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TurretData.Range); // Example radius
    }
}
