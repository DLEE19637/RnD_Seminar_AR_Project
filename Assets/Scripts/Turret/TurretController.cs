using System;
using UnityEngine;

[RequireComponent(typeof(TurretTargetManager))]
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
    private TurretTargetManager _turretTargetManager;

    [NonSerialized]
    public EnemyController Target;

    private float _shootingCooldown = 0f;
    private SphereCollider _detectionCollider;

    private void Awake()
    {
        _turretTargetManager = GetComponent<TurretTargetManager>();
        _detectionCollider = GetComponent<SphereCollider>();
        _detectionCollider.radius = TurretData.Range;
    }

    void Update()
    {
        Target = _turretTargetManager.ProvideTarget();
        if (Target == null)
        {
            return;
        }

        RotateToTarget();

        if (!IsTargetAligned())
        {
            return;
        }

        if (_shootingCooldown <= 0f)
        {
            Shoot();
            _shootingCooldown = 1f / TurretData.FireRate;
        }

        _shootingCooldown -= Time.deltaTime;
    }

    void RotateToTarget()
    {
        Vector3 dir = Target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(TurretRotation.rotation, lookRotation, Time.deltaTime * TurretData.RotationSpeed).eulerAngles;
        TurretRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    bool IsTargetAligned()
    {
        if (Target == null)
        {
            return false;
        }
        Vector3 dirToTarget = (Target.transform.position - TurretRotation.position).normalized;
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
            bulletController.SetTarget(Target.transform);
        }
        else
        {
            Debug.LogError($"{gameObject.name}." +
                $"{nameof(TurretController)}.{nameof(Shoot)}: " +
                $"BulletPrefab does not contain a BulletController");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TurretData.Range); // Example radius
    }
}
