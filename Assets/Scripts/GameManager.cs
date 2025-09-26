using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum GameState { Waiting, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; }
    public PlayerBase Base;
    public List<WayPoint> WayPoints = new List<WayPoint>();
    public EnemySpawner EnemySpawn;
    public bool HasBase => Base != null;
    public bool HasEnemySpawn => EnemySpawn != null;
    private int _currentWave;
	public int CurrentWave
    {
        get => _currentWave;
        set
        {
            _currentWave = value;
            UpdateWaveCounterText();
        }
    }

    public int RemainingEnemies;
    public bool IsGameWon = false;

    public TextMeshProUGUI WaveCounterText;


	public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Game state changed to: " + newState);
    }

    public void RegisterBase(PlayerBase playerBase)
    {
        if (Base != null)
        {
            Destroy(Base.gameObject);
        }
        Base = playerBase;
    }

    public void RegisterWayPoint(WayPoint wayPoint)
    {
        WayPoints.Add(wayPoint);
    }

    public void RegisterEnemySpawn(EnemySpawner enemySpawn) 
    {
        if(EnemySpawn != null)
        {
            Destroy(EnemySpawn.gameObject);
        }
        EnemySpawn = enemySpawn;
    }

    public void SetRemainingEnemies(int remainingEnemies)
    {
        RemainingEnemies = remainingEnemies;
    }

    public void RemoveEnemy()
    {
        RemainingEnemies--;
    }

    public void OnGameWin()
    {
        IsGameWon = true;
        Debug.Log("All waves cleared. User won!");
    }

    private void UpdateWaveCounterText()
    {
        WaveCounterText.text = $"Wave: {CurrentWave + 1}";
    }
}
