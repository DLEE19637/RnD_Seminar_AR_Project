using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class LevelData : ScriptableObject
{
    public List<WaveEntry> Enemies;
}
