using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject contBtn;
    public TMP_Text dialogueText;
    public string[] dialogue;
    private int index;
    public Item itemToDrop;
    public float dropDistance = 2.0f;


    public float wordSpeed;
    public bool playerIsClose;
    void Start()
    {
        dialogueText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if (dialogueText.text == dialogue[index])
        {
            contBtn.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.A) && dialoguePanel.activeInHierarchy)
        {
            {
                NextLine();
            }

        }
    }

    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }
    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contBtn.SetActive(false);
        if (MoneyManager.instane.current > 30)
        {

            if (index < dialogue.Length - 1)
            {
                index++;
                dialogueText.text = "";
                StartCoroutine(Typing());
            }
            else
            {
                DroppItem();

                ZeroText();
            }
        }
        else
        {
            dialogueText.text = "You need at less 30$ to me to drop random food";
            StartCoroutine(Typing());
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
            ZeroText();
        }
    }
    public void DroppItem()
    {
        MoneyManager.instane.DecreaseMoney(30);
        Vector3 dropPosition = new Vector3(transform.position.x + Random.Range(-dropDistance, dropDistance), transform.position.y, transform.position.z);
        Instantiate(GameManager.instance.itemManager.GetItemByName("Crop" + Random.Range(1, 9)), dropPosition, Quaternion.identity);
        Instantiate(itemToDrop, dropPosition + new Vector3(dropDistance, 0f, 0f), Quaternion.identity);
    }
}
