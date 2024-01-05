using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject settingPanel;
    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();
    public List<Inventory_UI> inventoryUIs;
    public static Slot_UI draggedSlot;
    public static Image draggegdIcon;
    public static bool dragSingle;
    private void Awake()
    {
        Initialize();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)|| Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventoryUI();
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt) )
        {
            ToggleSettingUI();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }
    }
    //Function to open and close inventory
    public void ToggleInventoryUI()
    {
        if (inventoryPanel != null)
        {
            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
                RefreshInventoryUI("Backpack");
            }
            else
            {
                inventoryPanel.SetActive(false);
            }
        }
    }
    public void ToggleSettingUI()
    {
        if (settingPanel != null)
        {
            if (!settingPanel.activeSelf)
            {
                settingPanel.SetActive(true);
                
            }
            else
            {
                settingPanel.SetActive(false);
            }
        }
    }

    public void RefreshInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            inventoryUIByName[inventoryName].Refresh();
        }
    }
    public void RefreshAll()
    {
        foreach(KeyValuePair<string, Inventory_UI> keyValuePair in inventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        }
    }

    void Initialize()
    {
        foreach(Inventory_UI ui in inventoryUIs)
        {
            if (!inventoryUIByName.ContainsKey(ui.inventoryName))
            {
                inventoryUIByName.Add(ui.inventoryName, ui);
            }
        }
    }

    public Inventory_UI GetInventoryUI(string inventoryName)
    {
        if(inventoryUIByName.ContainsKey(inventoryName)) return inventoryUIByName[inventoryName];
        Debug.LogWarning("There is not inventory UI for " + inventoryName);
        return null;
    }
}
