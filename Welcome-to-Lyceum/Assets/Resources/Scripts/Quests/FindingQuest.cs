using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class FindingQuest : MonoBehaviour
{
    public string questName;

    [SerializeField]
    private UnityEvent OnObjectFound = null;

    private QuestManager questManager;

    private void Start()
    { 
        questManager = QuestManager.instance;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var quests = questManager.quests;
            var currentQuest = quests.Find(t => t.name.Equals(questName));
            if (currentQuest.isActive)
            {
                questManager.QuestCompleted(questName);
                OnObjectFound.Invoke();
            }
        }
    }
}
