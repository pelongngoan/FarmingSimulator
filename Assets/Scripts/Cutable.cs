using System;
using UnityEditor.Rendering;
using UnityEngine;

public class Cutable : MonoBehaviour
{
    public bool noFruit;
    public bool smallTree;
    float timer;
    public Sprite noFruitPlant;
    public Sprite fruitPlant;
    public Sprite trunk;
    public SpriteRenderer plant;
    private float timeBtwStage = 30f;
    Player player;
    public Item treeLog;
    public Item apple;
    public int numOfItem;
    private bool playerIsClose;

    void Update()
    {
        if (noFruit)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = timeBtwStage;
                plant.sprite = fruitPlant;
                noFruit = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && playerIsClose)
        {
            player = GameManager.instance.player;
            if (player.inventoryManager.toolbar.selectedSlot.itemName == "Axe")
            {
                plant.sprite = trunk;
                if (!noFruit && !smallTree)
                {
                    for (int i = 0; i < numOfItem; i++)
                    {
                        Instantiate(treeLog, transform.position, Quaternion.identity);
                        Instantiate(apple, transform.position, Quaternion.identity);
                    }
                }
                else
                {
                    for (int i = 0; i < numOfItem; i++)
                    {
                        Instantiate(treeLog, transform.position, Quaternion.identity);

                    }
                }
                noFruit = true;
            }
            if (!smallTree)
            {
                if (!noFruit)
                {
                    plant.sprite = noFruitPlant;
                    noFruit = true;
                    for (int i = 0; i < 3; i++)
                    {
                        Instantiate(apple, transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                    }
                }
            }
        }
    }
    private void Start()
    {
        timer = timeBtwStage;
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
