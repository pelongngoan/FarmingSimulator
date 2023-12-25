using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    PlayerHealth playerHealth;
    public float healthBonus = 20f;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (playerHealth.healthBar.value < playerHealth.maxHealth)
        {
            Destroy(gameObject);
            playerHealth.healthBar.value = playerHealth.healthBar.value + healthBonus;
            playerHealth.curHealth = playerHealth.healthBar.value;
        }
    }
}