using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class CustomizationManager : MonoBehaviour
{
    public List<Color> Colors;
    public List<Slider> Sliders;
    public List<Slider> HairSliders;
    
    public List<SpriteRenderer> Parts;
    public List<Sprite> HairStyles;
    public List<Sprite> PartIcons;

    public List<GameObject> palettes;
    public GameObject prevPalette;
    
    public SpriteRenderer Skin;
    public SpriteRenderer Hair;

    private int PartsCounter = 0;
    private int HairCounter = 0;
    private Color[] colorsToSave;
    
    public Image PartIcon;
    public Image HairIcon;
    public Image PlayerButtonIcon;

    public TMP_Text inputName;

    private void Start()
    {
        palettes[0].SetActive(true);
    }

    void Update()
    {
        for (int i = 0; i < Parts.Count; i++)
        {
            Parts[i].color = Color.HSVToRGB(Sliders[i * 3].value, Sliders[i * 3 + 1].value, Sliders[i * 3 + 2].value);
        }

        PartIcon.color = Parts[PartsCounter].color;
        Hair.color = Color.HSVToRGB(HairSliders[0].value, HairSliders[1].value, HairSliders[2].value);
        HairIcon.color = Hair.color;
        PlayerButtonIcon.color = Parts[0].color;
    }
    
    public void ChooseSkinColor(int i)
    {
        Skin.color = Colors[i];
    }

    public void NextPart(int k)
    {
        prevPalette.SetActive(false);
        
        PartsCounter += k;
        if (PartsCounter < 0) PartsCounter = Parts.Count - 1;
        else if (PartsCounter >= Parts.Count) PartsCounter = 0;
        
        palettes[PartsCounter].SetActive(true);

        PartIcon.sprite = PartIcons[PartsCounter];
        PartIcon.color = Parts[PartsCounter].color;

        prevPalette = palettes[PartsCounter];
    }

    public void NextHair(int k)
    {
        HairCounter += k;
        
        if (HairCounter < 0) HairCounter = HairStyles.Count - 1;
        else if (HairCounter >= HairStyles.Count) HairCounter = 0;

        Hair.sprite = HairStyles[HairCounter];
        HairIcon.sprite = HairStyles[HairCounter];
    }

    public void Save()
    {
        colorsToSave = new Color[6];

        colorsToSave[0] = Skin.color;
        colorsToSave[1] = Hair.color;
        
        for (int i = 0; i < Parts.Count; i++)
        {
            colorsToSave[i + 2] = Parts[i].color;
        }

        var playerOutfitData = new PlayerOutfitData(HairCounter, colorsToSave);

        var formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerOutfit.gina";
        
        FileStream fileStream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(fileStream, playerOutfitData);
        fileStream.Close();
        
        PlayerPrefs.SetString("PlayerName", inputName.text);
        PlayerPrefs.SetInt("isSaved", 1);
        PlayerPrefs.Save();

        ES3.Save("PlayerOutfit", playerOutfitData);
        
        // var allParts = new List<SpriteRenderer>();
        // allParts.Add(Skin);
        // allParts.Add(Hair);
        // foreach (var part in Parts)
        // {
        //     allParts.Add(part);
        // }
        // ES3.Save("PlayerOutfit", allParts);
    }

}
