using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyData EnemyData;
    private EnemyMovement _enemyMovement;
    private Health _health;

    [System.NonSerialized]
    public UnityEvent<EnemyController> ReachedEnd = new();
    [System.NonSerialized]
    public UnityEvent<EnemyController> Died = new();

    public List<Transform> WayPoints
    {
        get => _enemyMovement.WayPoints;
        set => _enemyMovement.WayPoints = value;
    }

    void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyMovement.ReachedEnd.AddListener(OnReachedEnd);

        _health = GetComponent<Health>();
        _health.SetMaxHealth(EnemyData.Health);
        _health.Died.AddListener(OnDied);
    }

    public void Damage(int damage)
    {
        _health.Damage(damage);
    }

    private void OnDied()
    {
        Died.Invoke(this);

        Destroy(gameObject);
    }

    private void OnReachedEnd()
    {
        ReachedEnd.Invoke(this);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        _enemyMovement.Move(EnemyData.Speed);
    }
}
