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
    public Sprite[] plantStages;
    public float timeBtwStage = 2f;
    Player player;
    public Item potatoeSeedPack;
    [SerializeField] private List<CropData> cropData;
    /*private void Start()
    {
        inventory = GameManager.instance.player.inventoryManager.GetInventoryByName(inventoryName);

    }*/
    /*private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();

    }*/

    void Update()
    {
        if (isPlanted)
        {
            timer -= Time.deltaTime;
            if (timer < 0 && plantStage < plantStages.Length - 1)
            {
                timer = timeBtwStage;
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
        
        /*if (inventoryManager != null)
        {*/
        
        Debug.Log(player.inventoryManager.toolbar.selectedSlot.isSeed);
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
        timer = timeBtwStage;
        plant.gameObject.SetActive(false);
        GameManager.instance.player.DropItem(potatoeSeedPack, 3);
        chicken.gameObject.SetActive(true);
    }

    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
    }
}
