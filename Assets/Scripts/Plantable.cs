using UnityEngine;

public class Plantable : MonoBehaviour
{
    bool isWatered = false;
    int plantStage = 0;
    float timer;
    public SpriteRenderer chicken;
    public SpriteRenderer plant;
    public SpriteRenderer plowedDirt;
    public Sprite wateredDirt;
    public Sprite unWateredDirt;
    public Sprite[] plantStages;
    Player player;
    public CropData crop;
    public Item itemToDrop;
    public bool playerIsClose;


    void Update()
    {
        if (isWatered)
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
        if (Input.GetKeyDown(KeyCode.Space) && playerIsClose)
        {
            player = GameManager.instance.player;
            
            if (isWatered)
            {
                if (plantStage == plantStages.Length - 1)
                {
                    Harvest();
                }
            }
            else
            {
                if (player.inventoryManager.toolbar.selectedSlot != null)
                {
                    if (player.inventoryManager.toolbar.selectedSlot.isSeed)
                    {
                        crop = player.inventoryManager.toolbar.selectedSlot.crop;
                        plantStages = crop.plantStages;
                        itemToDrop = GameManager.instance.itemManager.GetItemByName(player.inventoryManager.toolbar.selectedSlot.itemName);
                        Plant();
                    }
                    if (player.inventoryManager.toolbar.selectedSlot.itemName == "WaterBottle")
                    {
                        Water();
                    }
                }
            }
        }
    }
    private void Plant()
    {
        plant.gameObject.SetActive(true);
        plant.sprite = plantStages[plantStage];
    }
    private void Water()
    {
        isWatered = true;
        plowedDirt.sprite = wateredDirt;

    }

    private void Harvest()
    {
        /*chicken.gameObject.SetActive(false);*/
        isWatered = false;
        plantStage = 0;
        UpdatePlant();
        timer = crop.timeBtwStage;
        plant.gameObject.SetActive(false);
        plowedDirt.sprite = unWateredDirt;
        /*GameManager.instance.player.DropItem(potatoeSeedPack, 3);*/
        chicken.gameObject.SetActive(false);
        Instantiate(itemToDrop, transform.position, Quaternion.identity);
    }

    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
