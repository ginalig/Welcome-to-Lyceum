using System;
using Resources.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    public void EnableDisable(GameObject other)
    {
        other.SetActive(!other.activeSelf);
    }

    public void SaveProgress()
    {
        var playerPosition = Player.Instance.transform.position;
        
        PlayerPrefs.SetString("CurrentLevelName", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("PlayerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", playerPosition.z);
        
        PlayerPrefs.Save();
        
        ES3.Save("PlayerPosition", playerPosition);
    }

}