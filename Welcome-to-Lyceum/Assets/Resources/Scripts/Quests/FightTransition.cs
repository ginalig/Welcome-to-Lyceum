using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightTransition : MonoBehaviour
{
    public Quests questsRef;
    
    public string sceneToLoad;
    public string questName;

    public bool isToStairs = false;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (isToStairs)
            {
                questsRef.sceneToLoad = sceneToLoad;
                questsRef.isAbleToLoad = true;
            }
            else if (questsRef.IsQuestActive(questName))
            {
                questsRef.sceneToLoad = sceneToLoad;
                questsRef.isAbleToLoad = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            questsRef.isAbleToLoad = false;
        }
    }
}
