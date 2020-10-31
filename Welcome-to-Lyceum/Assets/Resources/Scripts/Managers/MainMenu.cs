using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject continueButton = null;
    [SerializeField] private SceneLoader sceneLoader = null;
    [Header("MainQuests")]
    [SerializeField] private Quests defaultQuests = null;
    [SerializeField] private Quests quests = null;
    [Header("TutorialQuests")]
    [SerializeField] private Quests defaultTutorialQuests = null;
    [SerializeField] private Quests tutorialQuests = null;


    private void Start()
    {
        Time.timeScale = 1;
        
        if (PlayerPrefs.GetInt("isSaved", 0) == 1)
        {
            continueButton.SetActive(true);
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        foreach (var key in ES3.GetKeys())
        {
            ES3.DeleteKey(key);
        }

        tutorialQuests.SetQuests(defaultTutorialQuests);
        quests.SetQuests(defaultQuests);
    }
    
    public void Continue()
    {
        string sceneName = ES3.Load<string>("CurrentLevelName");
        sceneLoader.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
