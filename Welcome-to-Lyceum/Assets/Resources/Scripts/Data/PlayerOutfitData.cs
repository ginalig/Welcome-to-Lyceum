using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerOutfitData
{
    public int hairStyleNumber = 0;
    public float[] skinColor = new float[3]; // 0 - R. 1 - G. 2 - B.
    public float[] hairColor = new float[3];
    public float[] bodyColor = new float[3];
    public float[] backpackColor = new float[3];
    public float[] legsColor = new float[3];
    public float[] feetColor = new float[3];
    public float[,] allColors = new float[6, 3];

    public PlayerOutfitData()
    {
        hairStyleNumber = 0;
        for (int i = 0; i < 6; i++)
        {
            allColors[i, 0] = 0;
            allColors[i, 1] = 0;
            allColors[i, 2] = 0;
        }
    }
    
    public PlayerOutfitData(int hairStyleNumber, Color[] colors)
    {
        this.hairStyleNumber = hairStyleNumber;
        
        for (int i = 0; i < 6; i++)
        {
            allColors[i, 0] = colors[i].r;
            allColors[i, 1] = colors[i].g;
            allColors[i, 2] = colors[i].b;
        }
        
    }
}
