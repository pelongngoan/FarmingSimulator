using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap backgroundMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile plowedTile;
    [SerializeField] private Tile treeTile;
    [SerializeField] private Item treeLog;
    /*[SerializeField] private Tile[] croptableTile;*/
    public SpriteRenderer plant;

    //On start set the interactbable map to  be hidden
    void Start()
    {
        foreach(var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);
            /*if (tile != null && (tile.name == "Hills_11" || tile.name == "Hills_10" || tile.name == "Grass_5"))
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }*/
            if (tile != null && tile.name == "Basic Grass Biom things 1_1")
            {
                interactableMap.SetTile(position, treeTile);
            }
        }
        /*foreach (var position in backgroundMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = backgroundMap.GetTile(position);
            if (tile != null && tile.name == "Interactable Visible")
            {
                backgroundMap.SetTile(position, hiddenInteractableTile);
            }
            if (tile != null && tile.name == "Basic Grass Biom things 1_1")
            {
                backgroundMap.SetTile(position, treeTile);
            }
        }*/
    }
    public void SetPlowedTile(Vector3Int position)
    {

        interactableMap.SetTile(position, plowedTile);
    }
    public void SetSeedTile(Vector3Int position,Tile seedTile)
    {

        interactableMap.SetTile(position, seedTile);
    }
    /*public void SetWateredTile(Vector3Int position)
    {

        interactableMap.SetTile(position, wateredTile);
    }*/

    //Set the postion stand on to be interactable tile
    public void SetInteracted(Vector3Int position)
    {
        
        interactableMap.SetTile(position, plowedTile);    
    }


    public void SetTreeInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, plowedTile);
        GameManager.instance.player.DropItem(treeLog,3);
    }

    public string GetTileName(Vector3Int position)
    {
        if (interactableMap != null)
        {
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null)
            {
                return tile.name;
            }
         
        }
        return "";
    }
    public void PlantSeed(Vector3Int position, Tile seedTile)
    {
        if (interactableMap != null)
        {
            interactableMap.SetTile(position, seedTile);

            // Instantiate a Crop object at the planted position
            GameObject cropObject = new GameObject("Crop");
            cropObject.transform.position = interactableMap.GetCellCenterWorld(position);
            
        }
    }
}
