using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Inventory with number of slot
    public InventoryManager inventoryManager;
    private TileManager tileManager;
    private Animator animator;
    private AudioManager audioManager;
    public Sprite plant;
    public GameObject wateredDirt;
    PlayerHealth playerHealth;
    public Inventory_UI inventory_UI;
    public bool closeToTree;
    public bool closeToPlowedDirt;

    private void Start()
    {
        tileManager = GameManager.instance.tileManager;
        animator = gameObject.GetComponentInChildren<Animator>();
    }
    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        inventoryManager = GetComponent<InventoryManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    //Change the tile to interactable
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tileManager != null)
            {
                Vector3Int position = new Vector3Int((int)Math.Floor(transform.position.x),
                    (int)Math.Floor(transform.position.y - 0.5), 0);

                string tileName = tileManager.GetTileName(position);
                if (!string.IsNullOrWhiteSpace(tileName))
                {
                    if (
                        /*tileName == "Interactable Visible" */
                        (tileName == "Hills_11" || tileName == "Hills_10" || tileName == "Grass_5")
                        && inventoryManager.toolbar.selectedSlot.itemName == "Hoe")
                    {
                        Instantiate(wateredDirt, position, Quaternion.identity);
                        tileManager.SetPlowedTile(position);
                        animator.SetTrigger("isPlowing");
                        audioManager.PlaySFX(audioManager.dighoeClip);
                        /*inventoryManager.toolbar.selectedSlot.*/
                    }
                    if (closeToPlowedDirt && inventoryManager.toolbar.selectedSlot.itemName == "WaterBottle")
                    {
                        /*Instantiate(wateredDirt, position, Quaternion.identity);*/
                        animator.SetTrigger("isWatering");
                        audioManager.PlaySFX(audioManager.wateringClip);
                    }
                }
            }
            if (inventoryManager.toolbar.selectedSlot.eatable && playerHealth.healthBar.value < playerHealth.maxHealth)
            {

                audioManager.PlaySFX(audioManager.wateringClip);
                inventoryManager.toolbar.selectedSlot.RemoveItem();
                playerHealth.healthBar.value = playerHealth.healthBar.value + inventoryManager.toolbar.selectedSlot.healthBonus;
                playerHealth.curHealth = playerHealth.healthBar.value;
                GameManager.instance.uiManager.RefreshInventoryUI("Toolbar");
            }
            if (inventoryManager.toolbar.selectedSlot.itemName == "Axe" && closeToTree)
            {
                animator.SetTrigger("isCutting");
                audioManager.PlaySFX(audioManager.cutTreeClip);
            }
        }
        /* if (Input.GetMouseButtonDown(1))
         {
             if (inventoryManager.toolbar.selectedSlot.eatable) 
             {
                 Debug.Log("true");
             }

         }*/
        /*if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(worldPoint.GetType());
            if (tileManager != null)

            {
                Vector3Int position = new Vector3Int((int)Math.Floor(worldPoint.x),
                    (int)Math.Floor(worldPoint.y), 0);
                *//*string tileName = tileManager.GetTileName(position);
                if (!string.IsNullOrWhiteSpace(tileName))
                {
                    if (tileName == "Basic Grass Biom things 1_1" && inventoryManager.toolbar.selectedSlot.itemName == "Axe")
                    {

                        tileManager.SetTreeInteracted(position);
                        animator.SetTrigger("isCutting");
                        audioManager.PlaySFX(audioManager.cutTreeClip);
                    }
                    if (tileName == "Basic Grass Biom things 1_1" && inventoryManager.toolbar.selectedSlot.itemName == "PotatoeSeedPack")
                    {
                        tileManager.SetTreeInteracted(position);

                    }
                }*//*
                
            }
        }*/
    }

    //Drop Item to random location
    public void DropItem(Item item)
    {
        Vector3 spawnLocation = transform.position;

        Vector3 spawOffset = UnityEngine.Random.insideUnitCircle * 2.5f;

        Item droppedItem = Instantiate(item, spawnLocation + spawOffset, Quaternion.identity);
        //droppedItem.rb2d.AddForce(spawOffset*2f,ForceMode2D.Impulse);
    }

    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("NoFruit_AppleTree"))
        {
            closeToTree = true;
        }
        if (other.CompareTag("PlowedDirt"))
        {
            closeToPlowedDirt = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NoFruit_AppleTree"))
        {
            closeToTree = false;
        }
        if (other.CompareTag("PlowedDirt"))
        {
            closeToPlowedDirt = false;
        }
    }

}
