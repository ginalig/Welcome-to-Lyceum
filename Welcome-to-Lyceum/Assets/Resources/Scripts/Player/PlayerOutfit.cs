using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerOutfit : MonoBehaviour
{
    public List<Sprite> hairStyles;

    public List<SpriteRenderer> parts;

    private PlayerOutfitData playerOutfitData;

    private void Start()
    {

        playerOutfitData = ES3.Load("PlayerOutfit", new PlayerOutfitData());
        parts[1].sprite = hairStyles[playerOutfitData.hairStyleNumber]; // Загрузка определенной прически
        
        for (int i = 0; i < parts.Count; i++) // Придача цветов частям тела
        {
            parts[i].color = new Color(playerOutfitData.allColors[i, 0], 
                playerOutfitData.allColors[i, 1], playerOutfitData.allColors[i, 2]);
        }
    }

    /// <summary>
    /// Загрузка данных о внешнем виде 
    /// </summary>
    /// <returns></returns>
    public PlayerOutfitData LoadData() 
    {
        string path = Application.persistentDataPath + "/playerOutfit.gina";
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter(); 
            var fileStream = new FileStream(path, FileMode.Open);

            var data = formatter.Deserialize(fileStream) as PlayerOutfitData;
            
            fileStream.Close();

            return data;
        }

        Debug.Log("File missing");
        return null;
    }

}
