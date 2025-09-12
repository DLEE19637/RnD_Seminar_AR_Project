using UnityEngine;
using System.Collections.Generic;

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
}
