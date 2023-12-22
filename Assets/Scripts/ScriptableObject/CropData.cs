using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Crop Data",menuName ="Crop Data",order =50)]
public class CropData : ScriptableObject
{
    public string cropName = "Crop Name";
    public Sprite icon;
    public SpriteRenderer plant;
    public Sprite[] plantStages;
    int plantStage = 0;
    public float timeBtwStage ;
    float timer;
    [SerializeField] private Item potatoeSeedPack;

}
