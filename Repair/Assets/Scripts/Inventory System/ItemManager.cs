using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour, IInitable
{
    public List<ItemAsset> ItemAssets = new List<ItemAsset>();
    public static Dictionary<ItemType, ItemAsset> Items { get; private set; }

    public void Init()
    {
        Items = new Dictionary<ItemType, ItemAsset>();
        LoadItems();
    }

    void LoadItems()
    {
        Items.Clear();
        foreach (var item in ItemAssets)
            if (!Items.ContainsKey(item.Type))
                Items.Add(item.Type, item);
    }
}
