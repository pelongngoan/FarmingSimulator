using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    /*public InventoryManager inventoryManager;*/
    /*private TileManager tileManager;*/
    bool isPlanted = false;
    public SpriteRenderer plant;
    public SpriteRenderer chicken;
    public Sprite[] plantStages;
    int plantStage = 0;
    float timeBtwStage = 2f;
    float timer;
    [SerializeField] private ItemData potatoeSeedPack;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlanted)
        {
            timer -= Time.deltaTime;
            if (timer < 0 && plantStage<plantStages.Length-1)
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
            Plant();
        }
    }
    /*public void Inter()
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
            Plant();
        }
    }*/

    private void Plant()
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
        /*GameManager.instance.player.DropItem(potatoeSeedPack, 3);*/
    }

    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
    }
}
