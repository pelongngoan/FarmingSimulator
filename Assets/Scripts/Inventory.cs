using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    // Slot 
    public class Slot
    {
        public string itemName;
        public bool isSeed;
        public int count;
        public int maxAllowed;
        public Sprite icon;
        public CropData crop;
        public Slot()
        {
            itemName = "";
            count = 0;
            maxAllowed = 64;
        }
        public bool IsEmty
        {
            get
            {
                if (itemName == "" && count == 0)
                {
                    return true;
                }
                return false;
            }
        }
        public Sprite ItemIcon
        {
            get { return icon; }
        }
        //See if the item can be added
        public bool CanAddItem(string itemName)
        {
            if (this.itemName == itemName && count < maxAllowed)
            {
                return true;
            }
            return false;
        }
        //Added the item
        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.isSeed = item.data.isSeed;
            this.icon = item.data.icon;
            this.crop = item.data.crop;
            count++;
        }

        public void AddItem(string itemName, Sprite icon, int maxAllowed, bool isSeed,CropData crop)
        {
            this.itemName = itemName;
            this.icon = icon;
            this.isSeed = isSeed;
            this.crop = crop;

            count++;
            this.maxAllowed = maxAllowed;
        }
        //Remove the item
        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;
                if (count == 0)
                {
                    icon = null;
                    itemName = "";
                }
            }
        }
    }
    public List<Slot> slots = new List<Slot>();

    public Slot selectedSlot = null;
    //Inventory
    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }
    //Add item to inventory
    public void Add(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.itemName == item.data.itemName && slot.CanAddItem(item.data.itemName))
            {
                slot.AddItem(item);
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.itemName == "")
            {
                slot.AddItem(item);
                return;
            }
        }
    }
    //Remove item from inventory
    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }
    public void Remove(int index, int numToRemove)
    {
        if (slots[index].count >= numToRemove)
        {
            for (int i = 0; i < numToRemove; i++)
            {
                Remove(index);
            }
        }
    }
    public void MoveSlot(int fromIndex, int toIndex, Inventory toIventory, int numToMove = 1)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toIventory.slots[toIndex];
        if (toSlot.IsEmty || toSlot.CanAddItem(fromSlot.itemName))
        {
            for (int i = 0; i < numToMove; i++)
            {
                toSlot.AddItem(fromSlot.itemName, fromSlot.icon, fromSlot.maxAllowed, fromSlot.isSeed,fromSlot.crop);
                fromSlot.RemoveItem();
            }
        }
    }
    public void SelectSlot(int index)
    {
        if (slots != null && slots.Count > 0)
        {
            selectedSlot = slots[index];
        }
    }
}
