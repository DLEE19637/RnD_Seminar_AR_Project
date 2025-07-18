using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<WaveData> Waves;

    [SerializeField]
    private float waveInterval = 10f;
    private float countDown = 2f;

    private int currentWaveIndex = 0;

    private bool waveSpawning = false;
    private List<GameObject> aliveEnemies = new List<GameObject>();


    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        // If wave is in progress, check if all enemies are dead
        if (waveSpawning)
        {
            // Clean up any destroyed enemies
            aliveEnemies.RemoveAll(e => e == null);

            if (aliveEnemies.Count == 0)
            {
                waveSpawning = false;
                countDown = waveInterval; // start countdown to next wave
            }

            return;
        }

        // If we are between waves
        if (countDown > 0f)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0f)
            {
                StartCoroutine(StartNextWave());
            }
        }
    }


    IEnumerator StartNextWave()
    {
        if (currentWaveIndex >= Waves.Count)
        {
            Debug.Log("All waves completed.");
            yield break;
        }

        Debug.Log($"Spawning Wave {currentWaveIndex + 1}");
        yield return StartCoroutine(SpawnWave(Waves[currentWaveIndex]));
        waveSpawning = true;
        currentWaveIndex++;
    }

    IEnumerator SpawnWave(WaveData waveData)
    {
        for (int i = 0; i < waveData.Enemies.Count; i++)
        {
            StartCoroutine(SpawnWaveEntry(waveData.Enemies[i]));
        }

        yield break;
    }

    IEnumerator SpawnWaveEntry(WaveEntry entry)
    {
        for (int j = 0; j < entry.Count; j++)
        {
            GameObject enemy = SpawnEnemy(entry.EnemyData);
            aliveEnemies.Add(enemy);
            yield return new WaitForSeconds(entry.SpawnInterval);
        }
    }

    GameObject SpawnEnemy(EnemyData enemyData)
    {
        return Instantiate(enemyData.EnemyPrefab, transform.position, Quaternion.identity, transform);
    }
}
