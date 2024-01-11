using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instane;
    public TextMeshProUGUI moneyUI;

    public Transform Transform;
    [Header("Money")]
    public int current = 0;
    private void Awake()
    {
        instane = this;
    }
 
    public void IncreaseMoney(int amount)
    {
        current += amount;
        moneyUI.text = "$ : " +current; 
        
    }public void DecreaseMoney(int amount)
    {
        current -= amount;
        moneyUI.text = "$ : " +current; 
        
    }
}
