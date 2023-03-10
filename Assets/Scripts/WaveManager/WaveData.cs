using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Data/WaveData", order = 0)]
public class WaveData : ScriptableObject
{
    public List<SpawnInfo> SpawnInfos;
    public List<SpawnInfo> EliteSpawnInfos;
    [Range(0, 10f)]
    public float SpawnInterval;
    [Range(0, 100f)]
    public float EliteSpawnTime;
    [Range(0, 100)]
    public uint MaxUnitsAlive;
    public SpawnInfo GetRandomSpawnInfo()
    {
        return GetRandomSpawnInfo(SpawnInfos);
    }

    public SpawnInfo GetRandomEliteSpawnInfo()
    {
        return GetRandomSpawnInfo(EliteSpawnInfos);
    }

    private SpawnInfo GetRandomSpawnInfo(List<SpawnInfo> spawnInfos)
    {
        float totalWeight = 0;
        foreach (var spawnInfo in spawnInfos)
        {
            totalWeight += spawnInfo.Weight;
        }

        float randomValue = Random.value * totalWeight;
        foreach (var spawnInfo in spawnInfos)
        {
            if (randomValue < spawnInfo.Weight)
            {
                Debug.Log("1 Spawninfo.count: " + spawnInfos[0].Count);
                return spawnInfo;
            }

            randomValue -= spawnInfo.Weight;
        }

        Debug.Log("2 Spawninfo.count: " + spawnInfos[0].Count);
        return spawnInfos[0];
    }
}

[System.Serializable]
public struct SpawnInfo
{
    public GameObject EnemyPrefab;
    [Range(0, 30)]
    public uint Count;
    public float Weight;
    public bool IsGrouped;
}