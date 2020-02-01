using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        InventoryManager.MovingItem = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager.HoverItem(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.HoverItem(null);
    }
}
