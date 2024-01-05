using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Plantable : MonoBehaviour
{
    bool isPlanted = false;
    int plantStage = 0;
    float timer;
    public Sprite icon;
    public SpriteRenderer chicken;
    public SpriteRenderer plant;
    private Sprite[] plantStages;
    /*public float timeBtwStage = 2f*/
    Player player;
    /*public Item potatoeSeedPack;*/
    public CropData crop;
    /*[SerializeField] private List<CropData> cropData;*/
    /*private void Start()
    {
        inventory = GameManager.instance.player.inventoryManager.GetInventoryByName(inventoryName);

    }*/
    /*private void Awake()
    {
        crop =  

    }*/

    void Update()
    {
        if (isPlanted)
        {
            timer -= Time.deltaTime;
            if (timer < 0 && plantStage < plantStages.Length - 1)
            {
                timer = crop.timeBtwStage;
                plantStage++;
                UpdatePlant();
            }
            else if (plantStage == plantStages.Length - 1)
            {
                chicken.gameObject.SetActive(true);
            }
        }
    }
    private void OnMouseDown()
    {
        player = GameManager.instance.player;
        /*inventory = GameManager.instance.player.inventoryManager.GetInventoryByName(inventoryName);*/
        crop = player.inventoryManager.toolbar.selectedSlot.crop;
        plantStages = crop.plantStages;
        /*if (inventoryManager != null)
        {*/
        /*Sprite[] plantStage = crop.plantStages;*/
        Debug.Log(crop.cropName);
        Debug.Log(crop.timeBtwStage);

        if (player.inventoryManager.toolbar.selectedSlot.isSeed)
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
        /*}*/
    }
    private void Plant()
    {
        isPlanted = true;
        plant.gameObject.SetActive(true);
    }

    private void Harvest()
    {
        /*chicken.gameObject.SetActive(false);*/
        isPlanted = false;
        plantStage = 0;
        UpdatePlant();
        timer = crop.timeBtwStage;
        plant.gameObject.SetActive(false);
        /*GameManager.instance.player.DropItem(potatoeSeedPack, 3);*/
        chicken.gameObject.SetActive(false);
    }

    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
    }
}
