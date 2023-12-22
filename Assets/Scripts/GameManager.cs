using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;
    public TileManager tileManager;
    public CropManager cropManager;
    public UI_Manager uiManager;
    public Player player;

    private void Awake()
    {
        if (instance != null && instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        itemManager = GetComponent<ItemManager>();
        tileManager = GetComponent<TileManager>();
        cropManager = GetComponent<CropManager>();
        uiManager = GetComponent<UI_Manager>();
        player = FindObjectOfType<Player>();
    }
}
