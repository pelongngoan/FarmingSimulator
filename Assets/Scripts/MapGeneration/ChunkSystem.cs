using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkSystem : MonoBehaviour
{
    public Transform playerView;
    Vector3Int playerPos;
    int chunkSize;
    Dictionary<Vector3Int, Chunk> storeChunk = new Dictionary<Vector3Int, Chunk>();
    List<Chunk> pastChunks = new List<Chunk>();
    void Start() {
        //creeate map
        // add to dictionary
    }
    void Update() {
        //create new map base on direction of player
        //add those new map to dictionary
        ChunkManager();
    }
    void ChunkManager() {

    }
    public class Chunk {
        Vector3Int chunkPos;
        public Tilemap tileMapofChunk;
        public TileBase[] tileBases;
        List<GameObject> GOinChunk;
        Bounds bounds;
        public Chunk(Vector3Int chunkPos, int chunkSize,Tilemap tileMap, Transform parentPlaceholder) {
            
        }
    }
}
