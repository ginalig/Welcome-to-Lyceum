using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown = null;
    [SerializeField] private AudioMixer masterMixer = null;
    [SerializeField] private Toggle fullscreenToggle = null;

    private void Awake()
    {

        fullscreenToggle.isOn = Screen.fullScreen;
        
        resolutionDropdown.ClearOptions();    // Очистка всех вариантов выпадающего списка 
        
        var options = new List<string>();
        var currentResolutionIndex = 0;
        
        for (var i = 0; i < Screen.resolutions.Length; i++)
        {
            var resolution = Screen.resolutions[i];
            
            options.Add($"{resolution.width} x {resolution.height} ({resolution.refreshRate} Hz)");

            if (resolution.height == Screen.currentResolution.height &&
                resolution.width == Screen.currentResolution.width &&
                resolution.refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);    // Добавление списка опций в выпадающий список
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        var resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume(float value)
    {
        masterMixer.SetFloat("volume", value);
    }
}
