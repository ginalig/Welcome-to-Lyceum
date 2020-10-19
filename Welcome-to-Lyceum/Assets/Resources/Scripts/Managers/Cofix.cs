using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cofix : MonoBehaviour
{

    public Quests quests;

    public int restCharges;

    public Animator buttonAnimator;
    public GameObject triggerGameObject;

    public SceneLoader sceneLoader;

    public void Pipets()
    {
        StartCoroutine(DrinkCoffee());
    }
    
    public IEnumerator DrinkCoffee()
    {
        quests.AddRestCharges(restCharges);
        buttonAnimator.SetBool("IsButtonActive", false);
        yield return new WaitForSeconds(1.6f);
        buttonAnimator.gameObject.SetActive(false);
        triggerGameObject.gameObject.SetActive(false);
    }

    public void LoadBack()
    {
        var prevSceneName = ES3.Load<string>("CurrentLevelName");
        sceneLoader.LoadScene(prevSceneName);
    }
}
