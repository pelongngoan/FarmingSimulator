using System;
using UnityEditor.Rendering;
using UnityEngine;

public class Cutable : MonoBehaviour
{
    bool noFruit = true;
    float timer;
    public Sprite noFruitPlant;
    public Sprite fruitPlant;
    public Sprite trunk;
    public SpriteRenderer plant;
    public float timeBtwStage = 15f;
    Player player;
    public Item treeLog;
    private Animator animator;
    private AudioManager audioManager;
    private bool playerIsClose;

    void Update()
    {
        /*if (noFruit)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = timeBtwStage;
                UpdatePlant();
            }
            else if (plantStage == plantStages.Length - 1)
            {
                chicken.gameObject.SetActive(true);
            }
        }*/
        if (Input.GetKeyUp(KeyCode.Space) && playerIsClose)
        {
            player = GameManager.instance.player;
            if (player.inventoryManager.toolbar.selectedSlot.itemName == "Axe")
            {
                plant.sprite = trunk;
                Instantiate(treeLog, transform.position, Quaternion.identity);
            }
        }
    }
    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    private void OnMouseDown()
    {
        player = GameManager.instance.player;

        if (player.inventoryManager.toolbar.selectedSlot.itemName == "Axe")
        {
            /*Destroy(this.gameObject);*/
            /*animator.SetTrigger("isCutting");
            audioManager.PlaySFX(audioManager.cutTreeClip);*/
            plant.sprite = trunk;
            Instantiate(treeLog, transform.position, Quaternion.identity);
        }
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

    private void Harvest()
    {
        noFruit = true;
        timer = timeBtwStage;
        plant.sprite = noFruitPlant;
    }

    void UpdatePlant()
    {
        plant.sprite = fruitPlant;
    }
}
