using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{

    
    public string inventoryName;
    public List<Slot_UI> slots= new List<Slot_UI>();

    [SerializeField] private Canvas canvas;


    private bool dragSingle;

    private Inventory inventory;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }
    private void Start()
    {
        inventory = GameManager.instance.player.inventoryManager.GetInventoryByName(inventoryName);
        SetupSlots();

        Refresh();
    }
  
    
    //Refesth inventory UI
     public void Refresh()
    {
        if(slots.Count == inventory.slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if (inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }

            }
        }
        
    }
    //Remove item in UI
    public void Remove()
    {
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(
            inventory.slots[UI_Manager.draggedSlot.slotID].itemName);
        if (itemToDrop != null)
        {
            if (UI_Manager.dragSingle)
            {
                GameManager.instance.player.DropItem(itemToDrop);
                inventory.Remove(UI_Manager.draggedSlot.slotID);
                Refresh();
            }
            else
            {
                GameManager.instance.player.DropItem(itemToDrop, inventory.slots[UI_Manager.draggedSlot.slotID].count);
                inventory.Remove(UI_Manager.draggedSlot.slotID, inventory.slots[UI_Manager.draggedSlot.slotID].count);
                Refresh();
            }
            
        }
        UI_Manager.draggedSlot = null;
    }

    public void SlotBeginDrag(Slot_UI slot)
    {
        UI_Manager.draggedSlot = slot;
        UI_Manager.draggegdIcon = Instantiate(UI_Manager.draggedSlot.itemIcon);
        UI_Manager.draggegdIcon.transform.SetParent(canvas.transform);
        UI_Manager.draggegdIcon.raycastTarget = false;
        UI_Manager.draggegdIcon.rectTransform.sizeDelta = new Vector2(50, 50);
        MoveToMousePosition(UI_Manager.draggegdIcon.gameObject);
        
    }
    public void SlotDrag()
    {
        MoveToMousePosition(UI_Manager.draggegdIcon.gameObject);

        
    }
    public void SlotEndDrag()
    {
        Destroy(UI_Manager.draggegdIcon.gameObject);
        UI_Manager.draggegdIcon = null;
        
    }
    public void SlotDrop(Slot_UI slot)
    {
        if (UI_Manager.dragSingle)
        {
            UI_Manager.draggedSlot.inventory.MoveSlot(UI_Manager.draggedSlot.slotID, slot.slotID, slot.inventory);

        }
        else
        {
            UI_Manager.draggedSlot.inventory.MoveSlot(UI_Manager.draggedSlot.slotID, slot.slotID, slot.inventory,
                UI_Manager.draggedSlot.inventory.slots[UI_Manager.draggedSlot.slotID].count);

        }
        GameManager.instance.uiManager.RefreshAll();

    }
    private void MoveToMousePosition(GameObject toMove)
    {
        if (canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null , out position);
            toMove.transform.position = canvas.transform.TransformPoint(position);

        }
    }
    void SetupSlots()
    {
        int counter = 0;
        foreach (Slot_UI slot in slots)
        {
            slot.slotID = counter;
            counter++;
            slot.inventory = inventory;
        }
    }
}
