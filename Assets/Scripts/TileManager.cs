using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile plowedTile;
    [SerializeField] private Tile treeTile;
    [SerializeField] private Item treeLog;
    //On start set the interactbable map to  be hidden
    void Start()
    {
        foreach(var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null && tile.name== "Interactable Visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
            if (tile != null && tile.name == "Basic Grass Biom things 1_1")
            {
                interactableMap.SetTile(position, treeTile);
            }
        }
    }
    
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
}
