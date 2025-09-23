using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject EnemyPrefab;
    public string EnemyName;
    public int Health;
    public float Speed;
    public int KillReward;
    public EnemyType Type = EnemyType.Basic;
}

public enum EnemyType
{
    Basic,
    Speedy,
    Boss
}
