using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class FindingQuest : MonoBehaviour
{
    public string questName;

    [SerializeField] private Quests questsRef = null;
    [SerializeField] private UnityEvent OnObjectFound = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var quests = questsRef.quests;
            var currentQuest = quests.Find(t => t.name.Equals(questName));
            if (currentQuest.isActive)
            {
                questsRef.QuestCompleted(questName);
                OnObjectFound.Invoke();
            }
        }
    }
}
