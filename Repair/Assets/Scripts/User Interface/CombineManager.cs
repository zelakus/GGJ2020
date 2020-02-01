using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CombineManager : MonoBehaviour, IDropHandler, IInitable
{
    public static CombineManager Instance;
    public static GameObject MovingItem;
    public GridLayoutGroup Grid;
    public GameObject InventoryItem;

    public GameObject ItemPanel;
    public TMP_Text ItemName;
    public TMP_Text ItemInfo;

    readonly List<ItemSlot> Slots = new List<ItemSlot>();
    public void Init()
    {
        return;
        Instance = this;
        foreach (Transform slotTransform in Grid.transform)
        {
            var slot = slotTransform.GetComponent<ItemSlot>();
            slot.Index = Slots.Count;
            Slots.Add(slot);
        }
        Load();
    }

    void Update()
    {
        //Debug only!!!
        if (Input.GetKeyDown(KeyCode.U))
            Inventory.Serialize();
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

        //Replace
        Inventory.SetItem(from, t?.Type ?? ItemType.Nothing);
        SetUI(from, t);
        Inventory.SetItem(to, f?.Type ?? ItemType.Nothing);
        SetUI(to, f);
    }

    public void Drop(int index)
    {
        var item = Inventory.GetItem(index);
        if (item == null || item.Type == ItemType.Nothing)
            return;

        //Spawn drop object
        var player = GameObject.FindWithTag("Player");
        if (item.DropItem != null)
            Instantiate(item.DropItem, player.transform.position, Quaternion.identity);

        //Remove from inventory and update UI
        Inventory.RemoveAt(index);
        SetUI(index, null);
    }

    public void SetUI(int index, ItemAsset item)
    {
        //Remove old image
        if (Slots[index].transform.childCount != 0)
            Destroy(Slots[index].transform.GetChild(0).gameObject);

        //Create new image
        if (item != null && item.Type != ItemType.Nothing)
            Instantiate(InventoryItem, Slots[index].transform).GetComponent<Image>().sprite = item.Icon;
    }

    public void Load()
    {
        Inventory.Deserialize();
        for (int i=0;i<Inventory.Size;i++)
            SetUI(i, Inventory.GetItem(i));
    }

    public static void HoverItem(GameObject dragItem)
    {
        if (dragItem == null)
        {
            Instance.ItemPanel.SetActive(false);
        }
        else
        {
            var index = dragItem.transform.parent.GetComponent<ItemSlot>().Index;
            var item = Inventory.GetItem(index);
            if (item == null || item.Type == ItemType.Nothing)
                return;

            Instance.ItemName.SetText(item.Name);
            Instance.ItemInfo.SetText(item.Description);
            Instance.ItemPanel.SetActive(true);
        }
    }
}
