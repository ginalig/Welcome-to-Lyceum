using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private GameObject loadingScreen = null;
    [SerializeField] private Slider loadingBar = null;

    public static SceneLoader instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
        if (Time.timeScale < 1) Time.timeScale = 1;
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        
        while (!operation.isDone)
        {
            loadingBar.value = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }
    }
}
