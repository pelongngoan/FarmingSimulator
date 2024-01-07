using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    [SerializeField]
    TMP_Text healthText;

    public float maxHealth = 100;
    public float curHealth;

    void Start()
    {
        healthBar.value = maxHealth;
        curHealth = healthBar.value;
        StartCoroutine(AutoDecreaseHealth());
    }
    private IEnumerator AutoDecreaseHealth()
    {
        yield return new WaitForSeconds(1f);

        // Decrease health 
        DecreaseHealth(1f);

        StartCoroutine(AutoDecreaseHealth());
    }
    private void DecreaseHealth(float amount)
    {
        if (healthBar.value > 0.1)
        {
            healthBar.value -= amount;
            healthBar.value = Mathf.Max(healthBar.value, 0f);
            curHealth = healthBar.value;
        }
        else
        {
            StartCoroutine(WaitAndLoadScene());
        }

    }
    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(0);
    }
    
    private void Update()
    {
        healthText.text = curHealth.ToString() + "%";

    }
}

