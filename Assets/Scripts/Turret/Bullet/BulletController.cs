using UnityEngine;

[RequireComponent(typeof(BulletMovement))]
[RequireComponent(typeof(BulletHitBehaviourBase))]
public class BulletController : MonoBehaviour
{
    [SerializeField]
    private BulletData BulletData;
    private BulletMovement _bulletMovement;
    private BulletHitBehaviourBase _bulletHitBehaviour;
    private Transform _enemyTarget;

    void Awake()
    {
        _bulletMovement = GetComponent<BulletMovement>();
        _bulletMovement.Velocity = BulletData.Speed;

        _bulletHitBehaviour = GetComponent<BulletHitBehaviourBase>();
        _bulletHitBehaviour.Damage = BulletData.Damage;
    }

    public void SetTarget(Transform enemyTarget)
    {
        _enemyTarget = enemyTarget;
        _bulletMovement.SetTarget(enemyTarget);
    }

    // Update is called once per frame
    void Update()
    {
        if (_bulletMovement.NeedsTarget && _enemyTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        _bulletMovement.Move();
        if (_bulletMovement.HasReachedTarget())
        {
            _bulletHitBehaviour.Trigger(_enemyTarget);
        }
    }
}
