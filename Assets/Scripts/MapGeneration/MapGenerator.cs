using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{   
    public enum DrawMode {NoiseMap, ColourMap, Mesh, Tiles, FallOffMap};
    public const int mapChunkSize = 25;
    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale ;
    // int tempurator;
    // int altitude;
    //slider UI controls for persistance
    [Range(0,1)]
    public float persistance;
    public int octaves;
    public float lacunarity;
    public int seed;
    Vector2 offSet;
    public bool useFallOff;
    float[,] falloffMap;

    [SerializeField]
    public Tilemap tileMap;
    public tileBaseType[] tileBaseRegion;
    public TreeGenerator treeGenerator;
    public unspawnableTile unspawnableTiles;
    public MapData mapData;
    public bool autoUpdate;
   
    [System.Serializable]
    public struct tileBaseType {
        public string name;
        public float height;
        public bool unspawnableTile;
        public TileBase tile;
    }

    [System.Serializable]
    public struct TreeGenerator {
        public Transform playerPlaceHolder;
        public GameObject player;
	    public GameObject[] gameObject;
	    public Transform parent;
        public float radius;
	    public int density;
        public List<Vector2> points;
    }
    [System.Serializable]
    public struct unspawnableTile {
        public TileBase[] Tiles;
    }
    public struct MapData {
        public readonly float[,] heightMap;
        public MapData(float[,] heightMap)
        {
            this.heightMap = heightMap;
        }
    }
    void Start() {
        //GenerateBaseMapData();
        falloffMap = FallOffGenerator.GenerateFallOffMap(mapWidth);
        /*SpawnPlayer();*/
        System.Random random = new System.Random(); 
        treeGenerator.points = PoissonDiscSampling.GeneratePoints(tileMap.cellSize.x * Mathf.Sqrt(2)*treeGenerator.radius,  mapWidth , mapHeight,treeGenerator.density);
		if (treeGenerator.points != null) {
            foreach (Vector2 point in treeGenerator.points) {
            Vector3Int v3 = new Vector3Int((int)point.x,(int)point.y);
            Vector3 pos = tileMap.CellToWorld(v3);
            int iterator = random.Next(0,treeGenerator.gameObject.Length);
            if (SpawnRule(v3)) {
			Instantiate(treeGenerator.gameObject[iterator],pos,treeGenerator.parent.rotation,treeGenerator.parent);
            }
        }
    }
    void SpawnPlayer() {
         for (int i = 0; i < tileBaseRegion.Length; i++) {
            System.Random spawnPRNG  = new System.Random(); 
            int v3PosX = spawnPRNG.Next(0,mapWidth);
            int v3PosY = spawnPRNG.Next(0,mapHeight);
            Vector3Int pos = new Vector3Int(v3PosX,v3PosY);
            TileBase tb = tileMap.GetTile(pos);
            if (SpawnRule(pos)) {
                Instantiate(treeGenerator.player,pos,treeGenerator.playerPlaceHolder.rotation,treeGenerator.playerPlaceHolder);
                treeGenerator.player.AddComponent<CameraFollow>();
                break;
            }
         }
    }
    bool SpawnRule(Vector3Int v3) {
        TileBase tb = tileMap.GetTile(v3);
        for (int i = 0; i < unspawnableTiles.Tiles.Length; i++) {
                if (tb == unspawnableTiles.Tiles[i]) {
                    return false;
                }
            }
            return true;
        }
    }
    public void DrawMapInEditor() {
        if (drawMode == DrawMode.Tiles) {
            GenerateBaseMapData();
            //GenerateMoutainMap();
        }
    }
    //MAP DATA
    MapData GenerateBaseMapData() {
        float [,] noiseMap = Noise.GenerateNoiseMap(
            mapWidth, 
            mapHeight, 
            seed, 
            noiseScale,
            octaves, 
            persistance, 
            lacunarity,
            offSet
            );
             for (int y =0; y < mapHeight; y++) {
                for (int x = 0; x < mapWidth; x++) {
                    if (useFallOff) {
                        noiseMap[x,y] = Mathf.Clamp01(noiseMap[x,y] - falloffMap[x,y]);
                    }
                    float currentHeight = noiseMap [x,y];
                    for (int i = 0; i < tileBaseRegion.Length; i++) {
                    //loop through the regions to find which height the region fall into
                        if (currentHeight <= tileBaseRegion[i].height) {
                            Vector3Int v3 = new Vector3Int(x,y,0);
                            tileMap.SetTile(v3, tileBaseRegion[i].tile);
                            //break through the regions loop if the currentHeight satisfy the condition
                            break;
                        }   
                    }
                }         
            }
        return new MapData(noiseMap);
    }
        void OnValidate() {
        if (mapHeight < 1) {
            mapWidth = 1;
        }
        if (mapHeight < 1) {
            mapHeight = 1;
        }
        if (lacunarity < 1) { 
            lacunarity = 1;
        }
        if (octaves < 0) {
            octaves = 0;
        }
        falloffMap = FallOffGenerator.GenerateFallOffMap(mapWidth);   
        
    }

}
