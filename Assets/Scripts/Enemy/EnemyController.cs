using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

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
    [SerializeField]
    private ParticleSystem deadParticles;

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
        MoneyManager.Instance.ObtainEnemyReward(EnemyData.KillReward);
        deadParticles.transform.parent = null;
        deadParticles.Play();
        Destroy(deadParticles, 1f);
		Destroy(gameObject);
    }

    private void OnReachedEnd()
    {
        ReachedEnd.Invoke(this);

        ClearEvents();
        GameManager.Instance.RemoveEnemy();
        GameManager.Instance.LoseHealth();
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
