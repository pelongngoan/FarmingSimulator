using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public Crop[] crops;
    //A dictionary [key,values] to store item data
    private Dictionary<string, Crop> nameToCropDict =
        new Dictionary<string, Crop>();

    private void Awake()
    {
        foreach (Crop crop in crops)
        {
            AddCrop(crop);
        }
    }
    private void AddCrop(Crop crop)
    {
        if (!nameToCropDict.ContainsKey(crop.data.cropName))
        {
            nameToCropDict.Add(crop.data.cropName, crop);
        }
    }
    public Crop GetCropByName(string key)
    {
        if (nameToCropDict.ContainsKey(key))
        {
            return nameToCropDict[key];
        }
        return null;
    }

    // Start is called before the first frame update
    /*void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlanted)
        {
            timer -= Time.deltaTime;
            if (timer < 0 && plantStage<cropData[].plantStages.Length-1)
            {
                timer = timeBtwStage;
                plantStage++;
                UpdatePlant();
            } else if(plantStage == plantStages.Length - 1)
            {
                chicken.gameObject.SetActive(true);
            }
        }
    }
    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if (plantStage == plantStages.Length - 1)
            {
                Harvest();
            }
        }
        else
        {
            Plant(inventoryManager.toolbar.selectedSlot.itemName);
        }
    }

    private void Plant(string crop)
    {
        isPlanted = true;
        plant.gameObject.SetActive(true);
    }

    private void Harvest()
    {
        chicken.gameObject.SetActive(false);
        isPlanted = false;
        plantStage = 0;
        UpdatePlant();
        timer = timeBtwStage;
        plant.gameObject.SetActive(false);
        *//*GameManager.instance.player.DropItem(potatoeSeedPack, 3);*//*
    }

    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
    }*/
}
