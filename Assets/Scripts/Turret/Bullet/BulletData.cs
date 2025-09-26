using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
public class BulletData : ScriptableObject
{
    public GameObject BulletPrefab;
    public int Damage;
    public float Speed;
    public BulletType Type;
}

public enum BulletType
{
    Normal,
    Explosive,
    Piercing
}
