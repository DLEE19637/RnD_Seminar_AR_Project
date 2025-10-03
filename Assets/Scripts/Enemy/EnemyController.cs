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
    private EnemyHealthRegenerator _enemyHealthRegenerator;

    public float Speed;
    [System.NonSerialized]
    public UnityEvent<EnemyController> ReachedEnd = new();
    [System.NonSerialized]
    public UnityEvent<EnemyController> Died = new();

    void Start()
    {
        Speed = EnemyData.Speed;
        
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyMovement.ReachedEnd.AddListener(OnReachedEnd);

        _health = GetComponent<Health>();
        _health.SetMaxHealth(_enemyData.Health);
        _health.Died.AddListener(OnDied);

        _enemyHealthRegenerator = GetComponent<EnemyHealthRegenerator>();
    }

    public void Damage(float damage)
    {
        _health.Damage(damage);
    }

    private void OnDied()
    {
        Died.Invoke(this);

        ClearEvents();
		GameManager.Instance.RemoveEnemy();
		Destroy(gameObject);
    }

    private void OnReachedEnd()
    {
        ReachedEnd.Invoke(this);

        ClearEvents();
        GameManager.Instance.RemoveEnemy();
        Destroy(gameObject);
    }

    void Update()
    {
        _enemyMovement.Move(Speed);
    }

    private void ClearEvents()
    {
        Died.RemoveAllListeners();
        ReachedEnd.RemoveAllListeners();
        _enemyMovement.ReachedEnd.RemoveAllListeners();
        _health.Died.RemoveAllListeners();
    }
}
