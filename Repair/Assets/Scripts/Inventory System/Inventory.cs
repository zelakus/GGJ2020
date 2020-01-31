using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    //Private vars
    private readonly static ItemAsset[] Bag = new ItemAsset[12];
    private static uint _coin = 0;

    //Public vars
    public static uint Coin {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
            CoinsUpdated?.Invoke();
        }
    }
    public static Action CoinsUpdated;

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
            if (Bag[i].Type == type)
                indices.Add(i);
        }

        return indices;
    }

    //Item remove
    public static void RemoveAt(int index)
    {
        if (index > 0 && index < Bag.Length)
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

    //Save & Load
    //TODO
}
