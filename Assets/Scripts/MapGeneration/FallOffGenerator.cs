using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FallOffGenerator 
{
    public static float[,] GenerateFallOffMap(int size) {
        float[,] map = new float[size, size];
        for (int i =0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                //change the range from [0,1] -> [-1,1]
                float x = i/(float)size * 2 - 1;
                float y = j/(float)size * 2 - 1; 
                
                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                map[i,j] = Evalute(value);
            }
        }
        return map;
    }
    static float Evalute(float value)
 {
    float a = 4;
    float b = 4.4f;
     return Mathf.Pow(value,a)/(Mathf.Pow(value,a) + Mathf.Pow(b-b*value,a));
 }}
