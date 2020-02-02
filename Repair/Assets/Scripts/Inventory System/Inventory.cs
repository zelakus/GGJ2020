using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Inventory
{
    //Private vars
    private readonly static ItemAsset[] Bag = new ItemAsset[12];
    private static uint _parts = 0;

    //Public vars
    public static uint ColumnCount {
        get
        {
            return _parts;
        }
        set
        {
            _parts = value;
            ColumnUpdated?.Invoke();
        }
    }
    public static Action ColumnUpdated;
    public static int Size
    {
        get
        {
            return Bag.Length;
        }
    }

    //Item check
    public static bool HasItem(ItemType type)
    {
        for (int i = 0; i < Bag.Length; i++)
        {
            if (Bag[i].Type == type)
                return true;
        }

        return false;
    }

    //Item Get
    public static ItemAsset GetItem(int index)
    {
        if (index < 0 || index > Bag.Length)
            Debug.LogError($"Index is out of bounds. Given index: {index}. Inventory size: {Bag.Length}.");
        else
            return Bag[index];

        return null;
    }

    public static int GetItem(ItemType type)
    {
        for (int i = 0; i < Bag.Length; i++)
        {
            if (Bag[i].Type == type)
                return i;
        }

        return -1;
    }

    public static List<int> GetItems(ItemType type)
    {
        var indices = new List<int>();
        for (int i = 0; i < Bag.Length; i++)
        {
            if (Bag[i] != null && Bag[i].Type == type)
                indices.Add(i);
        }

        return indices;
    }

    //Item remove
    public static void RemoveAt(int index)
    {
        if (index >= 0 && index < Bag.Length)
        {
            Bag[index] = new ItemAsset(); //Empty Item = Empty slot
        }
        else
            Debug.LogError($"Index is out of bounds. Given index: {index}. Inventory size: {Bag.Length}.");
    }

    public static void RemoveAt(int[] indices)
    {
        if (indices != null)
        {
            for (int i = 0; i < indices.Length; i++)
                RemoveAt(indices[i]);
        }
        else
            Debug.LogError("Given indices array to remove is null.");
    }

    //Item add
    public static bool AddItem(ItemType type)
    {
        for (int i=0;i<Bag.Length;i++)
        {
            if (Bag[i].Type == ItemType.Nothing)
            {
                Bag[i] = ItemManager.Items[type];
                return true; //Item added
            }
        }

        return false; //Inventory is full
    }

    //Item set
    public static void SetItem(int index, ItemType type)
    {
        if (index >= 0 && index < Bag.Length)
        {
            if (ItemManager.Items.ContainsKey(type))
                Bag[index] = ItemManager.Items[type];
            else
                Bag[index] = null;
        }
        else
            Debug.LogError($"Index is out of bounds. Given index: {index}. Inventory size: {Bag.Length}.");
    }

    //Save & Load
    private static string SavePath
    {
        get
        {
            return Directory.GetParent(Application.dataPath).FullName + "\\inventory.dat";
        }
    }

    public static void Deserialize()
    {
        var json = File.ReadAllText(SavePath);
        var data = JsonUtility.FromJson<InventoryData>(json);
        for (int i = 0; i < data.Items.Length; i++)
        {
            SetItem(i, (ItemType)data.Items[i]);
        }
        ColumnCount = data.ColumnCount;
    }

    public static void Serialize()
    {
        var data = new InventoryData
        {
            Items = new int[Bag.Length],
            ColumnCount = ColumnCount
        };
        for (int i = 0; i < Bag.Length; i++)
            data.Items[i] = (int)(Bag[i]?.Type ?? 0);
        
        File.WriteAllText(SavePath, JsonUtility.ToJson(data, true));
    }
}
