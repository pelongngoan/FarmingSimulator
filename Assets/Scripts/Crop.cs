// Crop.cs
using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public int growthStages = 3;
    public float timeToHarvest = 10f;
    public Item yieldItem;

    private int currentStage = 0;

    private void Start()
    {
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        while (currentStage < growthStages)
        {
            yield return new WaitForSeconds(timeToHarvest / growthStages);
            // Update crop appearance or behavior for each growth stage
            currentStage++;
        }

        // The crop is fully grown, yield the item
        yieldItem = Instantiate(yieldItem);
        yieldItem.transform.position = transform.position;
        Destroy(gameObject);
    }
}
