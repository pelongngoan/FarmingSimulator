using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(
            int mapWidth, 
            int mapHeight,
            int seed, 
            float scale, //help zoom in and out
            int octaves,
            float persistance,
            float lacunarity,
            Vector2 offSet 
            ) {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        System.Random pseudoRNG = new System.Random(seed);      
        Vector2[] octaveOffset = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) {
            float offSetX = pseudoRNG.Next(-100000,100000) + offSet.x; //offSet.x: left right
            float offSetY = pseudoRNG.Next(-100000,100000) + offSet.y; //offSet.y: up down
            octaveOffset[i] = new Vector2(offSetX, offSetY);
        }
            if (scale <= 0) {
                scale = 0.0001f;
            }
            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;
            //halfW and halfH help zoom in and out in the center instead of the corners
            float halfWidth = mapWidth /2f;
            float halfHeight = mapHeight /2f;
                for (int y = 0; y < mapHeight; y++) {
                    for (int x = 0; x < mapWidth; x++) {

                        float amplitude = 1;
                        float frequency = 1;
                        float noiseHeight = 0;

                        for (int i = 0; i < octaves; i++) {
                        float sampleX = (x-halfWidth) / scale * frequency + octaveOffset[i].x;
                        float sampleY = (y-halfHeight) / scale * frequency + octaveOffset[i].y;
                        
                         //perlin noise product value in range of 0 -> 1, add more math equation for negative variety
                        float perlinValue = Mathf.PerlinNoise(sampleX,sampleY) * 2 - 1; 
                        noiseHeight += perlinValue * amplitude;

                        amplitude *= persistance; //amplitude decrease in each octaves
                        frequency *= lacunarity; // frequency increase in each octaves
                        }
                            if (noiseHeight > maxNoiseHeight) {
                                maxNoiseHeight = noiseHeight;
                            }
                            else if (noiseHeight < minNoiseHeight) { 
                                minNoiseHeight = noiseHeight;
                            }    
                            noiseMap[x, y] = noiseHeight;
                    }
                }
            for (int y = 0; y < mapHeight; y++) {
                for (int x = 0; x < mapWidth; x++) {
                    noiseMap[x,y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x,y]);
                }
            }    
            return noiseMap;
        }
}

