using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_UI : MonoBehaviour
{
    public int slotID;
    public Inventory inventory;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;

    [SerializeField] private GameObject highlight;
    //Set item to the slot
    public void SetItem(Inventory.Slot slot)
    {
        if (slot != null)
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();
        }
    }
    //Set slot to be empty
    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color  = new Color(1,1,1,0);
        quantityText.text="";
    }

    public void SetHighLight(bool isOn)
    {
        highlight.SetActive(isOn);
    }
}
