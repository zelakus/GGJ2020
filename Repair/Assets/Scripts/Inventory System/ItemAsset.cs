﻿using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Item")]
public class ItemAsset : ScriptableObject
{
    public ItemType Type = ItemType.Nothing;
    public string Name = "";
    public Sprite Icon;
    public string Description = "";
    public GameObject DropItem;
}
