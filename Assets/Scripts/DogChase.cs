using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    float timer;
    private float distance;
    public Animator animator;
    private float hungryTime = 10f;
    public SpriteRenderer hungryNote;
    private bool isHungry;
    private bool playerIsClose;
    private void Start()
    {
        timer = hungryTime;
    }
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            hungryNote.gameObject.SetActive(true);
            isHungry = true;
        }
        if (isHungry && playerIsClose && Input.GetKeyDown(KeyCode.Space) && GameManager.instance.player.inventoryManager.toolbar.selectedSlot.eatable)
        {
            timer = hungryTime;
            hungryNote.gameObject.SetActive(false);
            isHungry = false;
        }
        if (distance < 5 && distance >= 2)
        {

            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            animator.SetFloat("isMoving", 0.2f);
            animator.SetFloat("isBarking", 0.2f);
        }
        else if (distance < 2)
        {
            animator.SetFloat("isBarking", 0.0f);
        }
        else
        {
            animator.SetFloat("isMoving", 0.0f);
            animator.SetFloat("isBarking", 0.2f);
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

}
