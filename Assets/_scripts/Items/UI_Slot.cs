using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UI_Slot : MonoBehaviour, IDropHandler
{
    public Inventory_slot slot;
    public UI_Item ui_item;
    public int id;

    public void OnDrop(PointerEventData eventData)
    {
        UI_Item item = eventData.pointerDrag.GetComponent<UI_Item>();
        int added_amount = slot.CanAddItem(item.origin.slot.item, item.origin.slot.amount);
        ItemObject tmpItem = slot.item;
        SetContent(item.origin.slot.item, slot.amount + added_amount);
        item.origin.SetContent(tmpItem, item.origin.slot.amount - added_amount);
    }

    public void SetContent(ItemObject item, int amount)
    {
        slot.item = item;
        slot.amount = amount;
        ui_item.UpdateContent();
    }
}
