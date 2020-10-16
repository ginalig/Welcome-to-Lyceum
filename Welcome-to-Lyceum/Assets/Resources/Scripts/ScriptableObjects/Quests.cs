using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Quests")]
public class Quests : ScriptableObject
{
    public GameObject notificationPrefab = null;

    public GameEvent questCompleted;
    public List<Quest> quests;

    public bool isAbleToLoad = false;

    public string sceneToLoad;

    public string lastCompletedQuestName;

    public int restCharges;

    public void QuestCompleted(string questName)
    {
        var currentQuest = quests.Find(x => x.name.Equals(questName));
        currentQuest.isActive = false;
        
        int nextQuestIndex = quests.FindIndex(x => x.name.Equals(questName)) + 1;
        if (nextQuestIndex < quests.Count)
            quests[quests.FindIndex(x => x.name.Equals(questName)) + 1].isActive = true;

        lastCompletedQuestName = questName;
        
        questCompleted.Raise();
        currentQuest.OnQuestCompleted.Raise();
       
    }

    public bool IsQuestActive(string questName)
    {
        var quest = quests.Find(x => x.name.Equals(questName));
        return quest.isActive ? true : false;
    }
    
    public void ActivateQuest(string questName)
    {
        var quest = quests.Find(x => x.name.Equals(questName));
        quest.isActive = true;
    }
    
    public void DeactivateQuest(string questName)
    {
        var quest = quests.Find(x => x.name.Equals(questName));
        quest.isActive = false;
    }
}
