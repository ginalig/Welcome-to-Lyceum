using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    public QuestGoal questGoal;
    
    public string name;
    public string description;
    public string location;
    public string sceneName;
    public bool isActive;

    public UnityEvent OnQuestCompleted;

    public Quest(string name, string description, string location, string sceneName, bool isActive)
    {
        this.name = name;
        this.description = description;
        this.location = location;
        this.sceneName = sceneName;
        this.isActive = isActive;
    }
}
