using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.UI.InputField;

public class Player : MonoBehaviour
{
    // Inventory with number of slot
    public InventoryManager inventoryManager;
    private TileManager tileManager;
    private Animator animator;
    private AudioManager audioManager;
    public Sprite plant;
    public GameObject fruit;
    private void Start()
    {
        tileManager = GameManager.instance.tileManager;
        animator = gameObject.GetComponentInChildren<Animator>();
    }
    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        audioManager= GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    //Change the tile to interactable
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (tileManager != null)
            {
                Vector3Int position = new Vector3Int((int)Math.Floor(transform.position.x),
                    (int)Math.Floor(transform.position.y-0.5), 0);
                
                string tileName = tileManager.GetTileName(position);
                if (!string.IsNullOrWhiteSpace(tileName))
                {
                    Debug.Log(tileName);
                    Debug.Log(inventoryManager.toolbar.selectedSlot.itemName);
                    if (tileName == "Interactable Visible" && inventoryManager.toolbar.selectedSlot.itemName == "Hoe")
                    {
                        tileManager.SetPlowedTile(position);
                        animator.SetTrigger("isPlowing");
                        audioManager.PlaySFX(audioManager.dighoeClip);
                        /*inventoryManager.toolbar.selectedSlot.*/
                    }
                    if (tileName == "Summer_Plowed" && inventoryManager.toolbar.selectedSlot.itemName=="WaterBottle")
                    {
                        Instantiate(fruit, position, Quaternion.identity);
                        animator.SetTrigger("isWatering");
                        audioManager.PlaySFX(audioManager.wateringClip);
                    }
                    /* if (tileName == "PlowedTile" && inventoryManager.toolbar.selectedSlot.isSeed)
                     {
                         tileManager.PlantSeed(position, inventoryManager.toolbar.selectedSlot.ItemIcon);
                     }*/
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (tileManager != null)
            {
                Vector3Int position = new Vector3Int((int)Math.Floor(worldPoint.x),
                    (int)Math.Floor(worldPoint.y), 0);
                string tileName = tileManager.GetTileName(position);
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
                }
            }
        }
    }

    //Drop Item to random location
    public void DropItem(Item item)
    {
        Vector3 spawnLocation = transform.position;
       
        Vector3 spawOffset = UnityEngine.Random.insideUnitCircle*2.5f;

        Item droppedItem = Instantiate(item,spawnLocation+spawOffset,Quaternion.identity);
        //droppedItem.rb2d.AddForce(spawOffset*2f,ForceMode2D.Impulse);
    }

    public void DropItem(Item item, int numToDrop)
    {
       for(int i =0; i< numToDrop; i++)
        {
            DropItem(item);
        }
    }

}
