using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
    public bool isSeed = false;
   /* public CropData crop;
    public ItemData(string itemName, Sprite icon, bool isSeed, CropData crop = null)
    {
        this.itemName = itemName;
        this.icon = icon;
        this.isSeed = isSeed;

        if (isSeed)
        {
            if (crop == null)
            {
                // Handle the case when isCrop is true but crop is not provided
                Debug.LogError("CropData must be provided for crop items.");
            }
            else
            {
                this.crop = crop;
            }
        }
    }*/
}
/*[System.Serializable]
public class CropData
{
    public string cropName = "CropName";
    public Sprite icon;
    public SpriteRenderer plant;
    public Sprite[] plantStages;
    public float timeBtwStage;
    [SerializeField] private Item dropData;
}*/