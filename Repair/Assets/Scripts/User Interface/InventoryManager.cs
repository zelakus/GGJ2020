using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IDropHandler, IInitable
{
    public static GameObject MovingItem;
    public GridLayoutGroup Grid;

    readonly List<ItemSlot> Slots = new List<ItemSlot>();
    public void Init()
    {
        foreach (Transform slotTransform in Grid.transform)
        {
            var slot = slotTransform.GetComponent<ItemSlot>();
            slot.Index = Slots.Count;
            Slots.Add(slot);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (MovingItem == null)
            return;

        //Outer panel
        var panel = transform as RectTransform;
        if (!RectTransformUtility.RectangleContainsScreenPoint(panel,Input.mousePosition))
        {
            var index = MovingItem.transform.parent.GetComponent<ItemSlot>().Index;
            Drop(index);
        }

        //Grid slots
        foreach (RectTransform slot in Grid.transform)
            if (RectTransformUtility.RectangleContainsScreenPoint(slot, Input.mousePosition))
            {
                Debug.Log("Take " + MovingItem.name + " to " + slot.name);
                var from = MovingItem.transform.parent.GetComponent<ItemSlot>().Index;
                var to = slot.GetComponent<ItemSlot>().Index;
                if (from != to)
                    Move(from, to);
            }


        MovingItem = null;
    }

    public void Move(int from, int to)
    {
        var f = Inventory.GetItem(from);
        var t = Inventory.GetItem(to);

        if (f == null || t == null)
            return;

        //Replace
        //TODO
    }

    public void Drop(int index)
    {
        var item = Inventory.GetItem(index);
        if (item == null || item.Type == ItemType.Nothing)
            return;

        //TODO Instantiate 3d "drop item" object

    }
}
