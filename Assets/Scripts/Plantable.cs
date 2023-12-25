using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantable : MonoBehaviour
{
    public string inventoryName;
    bool isPlanted = false;
    int plantStage = 0;
    float timer;
    private Inventory inventory;
    Crop cropToPlant;
    InventoryManager inventoryManager;


    private void Start()
    {
        
    
    }
    void Update()
    {
        if (isPlanted)
        {
            timer -= Time.deltaTime;
            if (timer < 0 && plantStage < cropToPlant.data.plantStages.Length - 1)
            {
                timer = cropToPlant.data.timeBtwStage;
                plantStage++;
                UpdatePlant();
            }
            /*else if (plantStage == cropToPlant.data.plantStages.Length - 1)
            {
                chicken.gameObject.SetActive(true);
            }*/
        }
    }
    private void OnMouseDown()
    {
        inventory = GameManager.instance.player.inventoryManager.GetInventoryByName(inventoryName);
        inventoryManager = GetComponent<InventoryManager>();
        cropToPlant = GameManager.instance.cropManager.GetCropByName(
            inventory.slots[UI_Manager.draggedSlot.slotID].itemName);
        Debug.Log(inventory );
        Debug.Log(cropToPlant);
        if (inventoryManager != null)
        {
            if (inventoryManager.toolbar.selectedSlot.isSeed)
            {
                if (isPlanted)
                {
                    if (plantStage == cropToPlant.data.plantStages.Length - 1)
                    {
                        Harvest();
                    }
                }
                else
                {
                    Plant();
                }
            }
        }
    }
    private void Plant()
    {
        isPlanted = true;
        cropToPlant.data.plant.gameObject.SetActive(true);
    }

    private void Harvest()
    {
        /*chicken.gameObject.SetActive(false);*/
        isPlanted = false;
        plantStage = 0;
        UpdatePlant();
        timer = cropToPlant.data.timeBtwStage;
        cropToPlant.data.plant.gameObject.SetActive(false);
        /*GameManager.instance.player.DropItem(potatoeSeedPack, 3);*/
    }

    void UpdatePlant()
    {
        cropToPlant.data.plant.sprite = cropToPlant.data.plantStages[plantStage];
    }
}
