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
    private float _waveInterval = 5f;
    private float _timer = 0;

    public int _currentWaveIndex = 0;
    private readonly int _currentLevelIndex = 0;

    // Update is called once per frame

    private void Awake()
    {
        _timer = _waveInterval;
    }

	private void Start()
	{
        GameManager.Instance.RegisterEnemySpawn(this);
		DrawWayPoints();
	}

	private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0)
        {
            return;
        }

        SpawnTotalWave();
        //StartSpawning(Levels[_currentLevelIndex].Enemies[_currentWaveIndex]);
        _timer = _waveInterval;
    }

    private void SpawnTotalWave()
    {
        foreach(var waveEntry in Levels[_currentLevelIndex].Enemies)
        {
            StartSpawning(waveEntry);
        }
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
        var enemyObject = Instantiate(enemy, transform.position, Quaternion.identity);
        return enemyObject.GetComponent<EnemyController>();
    }

    private void DrawWayPoints()
    {
        for (int i = 0; i < GameManager.Instance.WayPoints.Count - 1; ++i)
        {
            Debug.DrawLine(GameManager.Instance.WayPoints[i].transform.position, GameManager.Instance.WayPoints[i + 1].transform.position);
        }
    }
}