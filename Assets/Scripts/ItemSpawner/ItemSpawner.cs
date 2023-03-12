using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ItemSpawner : MonoBehaviour
{
    [Header("Item Spawn Settings")]
    [ReadOnly]
    [ShowInInspector]
    private int _itemCount = 0;
    [SerializeField] private List<Vector2> _spawnOffsets = new List<Vector2>(); // Stores local positions of the spawn anchors
    
    private WaveManager _waveManager;

    public ItemPoolData itemPoolData;

    
    private void OnValidate() // Called when a value is changed in the inspector
    {
        _itemCount = _spawnOffsets.Count;
    }

    private void Awake()
    {
        Enemy enemy = GetComponent<Enemy>();
        enemy.died.AddListener(() => SpawnItems());
        _itemCount = _spawnOffsets.Count; 
        _waveManager = FindObjectOfType<WaveManager>();
        if (_waveManager == null)
        {
            Debug.LogError("No WaveManager found in scene, disabling ItemSpawner");
            enabled = false;
        }
    }
    private void SpawnItems()
    {
        var testnum = Random.Range(0f, 1f);
        if (testnum > itemPoolData.spawnProb)
        {
            return;
        }
        
        GameObject upgradeGroupObj = new GameObject("UpgradeDropGroup", typeof(UpgradeDropGroup));
        upgradeGroupObj.transform.position = transform.position;
        upgradeGroupObj.transform.rotation = Quaternion.identity;
        UpgradeDropGroup upgradeDropGroup = upgradeGroupObj.GetComponent<UpgradeDropGroup>();
        
        
        List<GameObject> items = itemPoolData.GetRandomItems(_itemCount);
        for (int i = 0; i < _itemCount; i++)
        {
            upgradeDropGroup.InstantiateUpgradeDrop(items[i], _spawnOffsets[i]);
        }
    }
}
