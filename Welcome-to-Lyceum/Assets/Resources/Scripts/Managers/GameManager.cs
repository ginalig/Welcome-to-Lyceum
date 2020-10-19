using System;
using Resources.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Quests questsRef;
    public Position playerPosition;
    private Scene currentScene;

    // private void Awake()
    // {
    //     currentScene = SceneManager.GetActiveScene();
    //
    //     if (currentScene.name.Equals("MainMenu") ||
    //         currentScene.name.Equals("SettingsMenu"))
    //     {
    //         gameObject.SetActive(false);
    //     }        
    //     else if (instance == null)
    //     {
    //         instance = this;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }
    // }

    private void Start()
    {
    }

    public void EnableDisable(GameObject other)
    {
        other.SetActive(!other.activeSelf);
    }

    public void SaveProgress()
    {
        
        PlayerPrefs.SetString("CurrentLevelName", currentScene.name);
        

        ES3.Save("CurrentLevelName",SceneManager.GetActiveScene().name);
        ES3.Save("PlayerPosition", playerPosition);
        ES3.Save("Quests", questsRef);
    }

}