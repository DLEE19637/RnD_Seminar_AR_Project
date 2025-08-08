using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "Scriptable Objects/TurretData")]
public class TurretData : ScriptableObject
{
    public GameObject TurretPrefab;
    public string Name;
    public int Cost;
    public float Range;
    public float FireRate;
    public float RotationSpeed;
}
