using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public UnityEvent<EnemyController> OnSpawnEnemy;

    [SerializeField]
    private List<LevelData> Levels;


    [SerializeField]
    private float _waveInterval = 10f;
    private float _timer = 0;

    private readonly int _currentWaveIndex = 0;
    private readonly int _currentLevelIndex = 0;

    [SerializeField]
    private List<Transform> Waypoints = new();

    // Update is called once per frame

    private void Awake()
    {
        _timer = _waveInterval;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0)
        {
            return;
        }

        StartSpawning(Levels[_currentLevelIndex].Enemies[_currentWaveIndex]);
        _timer = _waveInterval;
    }

    private void StartSpawning(WaveEntry waveEntry)
    {
        StartCoroutine(SpawnWave(waveEntry));
    }

    private IEnumerator SpawnWave(WaveEntry waveEntry)
    {
        for (int i = 0; i < waveEntry.Count; ++i)
        {
            OnSpawnEnemy.Invoke(SpawnEnemy(waveEntry.EnemyData.EnemyPrefab));
            yield return new WaitForSeconds(waveEntry.SpawnInterval);
        }
    }

    private EnemyController SpawnEnemy(GameObject enemy)
    {
        var enemyObject = Instantiate(enemy, Waypoints.First());
        var enemyController = enemyObject.GetComponent<EnemyController>();
        enemyController.WayPoints = Waypoints;
        return enemyController;
    }
}
