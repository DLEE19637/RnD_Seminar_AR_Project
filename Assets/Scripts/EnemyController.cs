using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyData _enemyData;
    public EnemyData EnemyData { get => _enemyData; }
    private EnemyMovement _enemyMovement;
    private Health _health;

    [System.NonSerialized]
    public UnityEvent<EnemyController> ReachedEnd = new();
    [System.NonSerialized]
    public UnityEvent<EnemyController> Died = new();

    public List<Transform> WayPoints { get; set; }

    void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyMovement.ReachedEnd.AddListener(OnReachedEnd);
        _enemyMovement.WayPoints = WayPoints;

        _health = GetComponent<Health>();
        _health.SetMaxHealth(_enemyData.Health);
        _health.Died.AddListener(OnDied);
    }

    public void Damage(int damage)
    {
        _health.Damage(damage);
    }

    private void OnDied()
    {
        Died.Invoke(this);

        ClearEvents();
        Destroy(gameObject);
    }

    private void OnReachedEnd()
    {
        ReachedEnd.Invoke(this);

        ClearEvents();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        _enemyMovement.Move(_enemyData.Speed);
    }

    private void ClearEvents()
    {
        Died.RemoveAllListeners();
        ReachedEnd.RemoveAllListeners();
        _enemyMovement.ReachedEnd.RemoveAllListeners();
        _health.Died.RemoveAllListeners();
    }
}
