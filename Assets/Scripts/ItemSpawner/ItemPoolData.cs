using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemPoolData", menuName = "Data/ItemPoolData", order = 0)]
public class ItemPoolData : ScriptableObject
{
    public GameObject Item;
    [SerializeField] List<ItemPoolElement> _items = new();
    [SerializeField] public float spawnProb;

    public List<GameObject> GetRandomItems(int count)
    {
        HashSet<ItemPoolElement> itemSet = new(_items); // Create a set of items so we can remove items from it
        List<GameObject> result = new();
        for (int i = 0; i < count; i++)
        {
            ItemPoolElement selected = GetRandomItemPoolElement(itemSet);
            itemSet.Remove(selected); // Remove the selected item from the set so it can't be selected again'
            if (itemSet.Count == 0)
            {
                itemSet = new HashSet<ItemPoolElement>(_items); // If we've run out of items, reset the set
            }
            Item.GetComponent<Upgrade>().upgradeData = selected.UpgradeData;
            result.Add(Item);
        }
        return result;
    }


    public ItemPoolElement GetRandomItemPoolElement(HashSet<ItemPoolElement> items)
    {
        if (items.Count == 0)
        {
            Debug.LogError("No items in pool");
            return default;
        }
        float totalWeight = 0;
        foreach (ItemPoolElement item in items)
        {
            totalWeight += item.Weight;
        }
        float randomValue = Random.value * totalWeight;
        foreach (ItemPoolElement item in items)
        {
            if (randomValue < item.Weight)
            {
                return item;
            }
            randomValue -= item.Weight;
        }
        return default;
    }
}

[System.Serializable]
public struct ItemPoolElement
{
    public UpgradeData UpgradeData;
    public float Weight;
}

