using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Data/WaveData", order = 0)]
public class WaveData : ScriptableObject
{
    public List<SpawnInfo> SpawnInfos;
    public float SpawnInterval;
    public float Duration;
    public SpawnInfo GetRandomSpawnInfo()
    {
        float totalWeight = 0;
        foreach (var spawnInfo in SpawnInfos)
        {
            totalWeight += spawnInfo.Weight;
        }

        float randomValue = Random.value * totalWeight;
        foreach (var spawnInfo in SpawnInfos)
        {
            if (randomValue < spawnInfo.Weight)
            {
                return spawnInfo;
            }

            randomValue -= spawnInfo.Weight;
        }

        return SpawnInfos[SpawnInfos.Count - 1];
    }
}

[System.Serializable]
public struct SpawnInfo
{
    public GameObject EnemyPrefab;
    public int Count;
    public float Weight;
    public bool IsGrouped;
}