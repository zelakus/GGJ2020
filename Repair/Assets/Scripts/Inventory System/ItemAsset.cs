using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Item")]
public class ItemAsset : ScriptableObject
{
    public ItemType Type = ItemType.Nothing;
    public string Name = "";
    public Sprite Icon;
    [TextArea(3,10)]
    public string Description = "";
    public int Price = 0;
    public GameObject DropItem;
}
