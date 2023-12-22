using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item Data", menuName ="Item Data", order=50)]
public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
    public bool isCrop;
    public string cropName = "Crop Name";
    public SpriteRenderer plant;
    public Sprite[] plantStages;
    int plantStage = 0;
    public float timeBtwStage;
    float timer;
}
