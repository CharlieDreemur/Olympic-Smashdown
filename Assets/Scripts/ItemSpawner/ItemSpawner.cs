using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ItemSpawner : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("This data should contain a list of items and their weights. The weights determine the probability of an item being selected.")]
    [Required]
    [AssetsOnly]
    [SerializeField] private ItemPoolData _itemPoolData;
    [Space(10)]

    [Header("Item Spawn Settings")]
    [ReadOnly]
    [ShowInInspector]
    private int _itemCount = 0;
    [SerializeField] private List<Vector2> _spawnOffsets = new List<Vector2>(); // Stores local positions of the spawn anchors


    private void OnValidate() // Called when a value is changed in the inspector
    {
        _itemCount = _spawnOffsets.Count;
    }

    private void Awake()
    {
        Enemy enemy = GetComponent<Enemy>();
        enemy.died.AddListener(() => SpawnItems());
    }
    private void SpawnItems()
    {
        List<GameObject> items = _itemPoolData.GetRandomItems(_itemCount);
        for (int i = 0; i < _itemCount; i++)
        {
            Instantiate(items[i], transform.position + new Vector3(_spawnOffsets[i].x, _spawnOffsets[i].y, 0), Quaternion.identity);
        }
    }
}
