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

    private bool waveSpawned = false;
    private bool hasGameEnded = false;

    private void Awake()
    {
        _timer = _waveInterval;
    }

	private void Start()
	{
        GameManager.Instance.RegisterEnemySpawn(this);
        GameManager.Instance.CurrentWave = 0;
		GameManager.Instance.SetRemainingEnemies(Levels[GameManager.Instance.CurrentWave].Enemies.Select(wave => wave.Count).Sum());
	}

	private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0 || GameManager.Instance.IsGameWon)
        {
            return;
        }

        if(GameManager.Instance.RemainingEnemies > 0 && !waveSpawned)
        {
			StartSpawning(Levels[GameManager.Instance.CurrentWave]);
            waveSpawned = true;
            return;
		} 

        if (GameManager.Instance.RemainingEnemies == 0 &&
            Levels.Count - 1 >= GameManager.Instance.CurrentWave) 
        {
			GameManager.Instance.CurrentWave++;

            if(Levels.Count == GameManager.Instance.CurrentWave)
            {
				GameManager.Instance.OnGameWin();
				GameManager.Instance.CurrentWave--;
			}
            else
            {
				GameManager.Instance.SetRemainingEnemies(Levels[GameManager.Instance.CurrentWave].Enemies.Select(wave => wave.Count).Sum());
				waveSpawned = false;
			} 
        }
	}

    private void StartSpawning(LevelData level)
    {
        level.Enemies.ForEach(wave =>
        {
			StartCoroutine(SpawnWave(wave));
		});
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
}