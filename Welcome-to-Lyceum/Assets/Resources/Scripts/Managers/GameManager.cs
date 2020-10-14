using System;
using Resources.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Quests questsRef = null;

    private void Awake()
    {
        var currentScene = SceneManager.GetActiveScene();

        if (currentScene.name.Equals("MainMenu") ||
            currentScene.name.Equals("SettingsMenu"))
        {
            gameObject.SetActive(false);
        }        
        else if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
    }

    public void EnableDisable(GameObject other)
    {
        other.SetActive(!other.activeSelf);
    }

    public void SaveProgress()
    {
        var playerPosition = Player.Instance.transform.position;
        
        PlayerPrefs.SetString("CurrentLevelName", SceneManager.GetActiveScene().name);
        
        PlayerPrefs.Save();
        
        ES3.Save("PlayerPosition", playerPosition);
        ES3.Save("Quests", questsRef);
    }

}