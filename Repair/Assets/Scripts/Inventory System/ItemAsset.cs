using UnityEngine;

public class ItemAsset : ScriptableObject
{
    public ItemType Type = ItemType.Nothing;
    public string Name = "";
    public Texture2D Icon;
}
