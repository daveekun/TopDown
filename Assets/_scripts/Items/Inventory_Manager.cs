using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory_slot
{
    public int amount;
    public ItemObject item;

    public Inventory_slot()
    {
        amount = 0;
        item = null;
    }

    public int CanAddItem(ItemObject _item, int _amount)
    {
        if (item == null)
        {
            return _amount;
        }
        if (item.id == _item.id)
        {
            if (item.stackable)
            {
                if (amount + _amount <= GameManager.MAX_STACK)
                {
                    return _amount;
                }
                else
                {
                    return (GameManager.MAX_STACK - amount);
                }
            }
            else
            {
                return 0;
            }
        }
        return 0;
    }
}

public class Inventory_Manager : MonoBehaviour
{
    public int size;
    public bool bag_is_open = false;
    public GameObject Bag;
    public List<Inventory_slot> slots;
    public List<UI_Slot> ui_slots;
    public List<UI_Item> ui_items;

    void changeBag()
    {
        bag_is_open = !bag_is_open;
        Bag.SetActive(bag_is_open);
    }

    void Awake()
    {
        Bag.SetActive(true);
        PlayerInput.onBagChange += changeBag;
        if (size != slots.Count)
        {
            Debug.LogError("Mismatched size and inventory slots");
            return;
        }
        ui_items = new List<UI_Item>();
        ui_slots = new List<UI_Slot>();
        UI_Slot[] slotObjects = GameObject.FindObjectsOfType<UI_Slot>();
        UI_Item[] itemObjects = GameObject.FindObjectsOfType<UI_Item>();
        if (slotObjects.Length != size || itemObjects.Length != size)
        {
            Debug.LogError("Mismatched size and ui slots");
            return;
        }

        for (int i = 0; i < size; i++)
        {
            UI_Slot slot_add = slotObjects[i];
            UI_Item item_add = itemObjects[i];
            ui_slots.Add(slot_add);
            ui_items.Add(item_add);
        }
        ui_slots.Sort((s1, s2) => s1.id.CompareTo(s2.id));
        for (int i = 0; i < size; i++)
        {
            UI_Slot slot = ui_slots[i];
            slot.ui_item = slot.gameObject.GetComponentInChildren<UI_Item>();
            slot.slot = slots[i];
            slot.ui_item.Awake();
        }
        ui_items.Sort((s1, s2) => s1.id.CompareTo(s2.id));
        Bag.SetActive(bag_is_open);
    }

    public int AddItem(ItemObject item, int amount)
    {
        for(int i = 0; i < size; i++)
        {
            if (slots[i].item != null)
            {
                int canAdd = slots[i].CanAddItem(item, amount);
                if (canAdd != 0)
                {
                    slots[i].amount += canAdd;
                    ui_items[i].UpdateContent();
                    return (canAdd + AddItem(item, amount - canAdd));
                }
            }
        }
        for (int i = 0; i < size; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = item;
                slots[i].amount = amount;
                ui_items[i].UpdateContent();
                return (amount);
            }
        }
        return 0;
    }
}
