using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Quests")]
public class Quests : ScriptableObject
{
    public List<Quest> quests;

    public bool isAbleToLoad = false;

    public string sceneToLoad;

    public string lastCompletedQuestName;

    public int restCharges;

    public int homeRestCooldown;
    public int otherRestCooldown;

    public GameEvent questCompleted;
    

    public void QuestCompleted(string questName)
    {
        var currentQuest = quests.Find(x => x.name.Equals(questName));
        currentQuest.isActive = false;
        
        int nextQuestIndex = quests.FindIndex(x => x.name.Equals(questName)) + 1;
        if (nextQuestIndex < quests.Count)
            quests[quests.FindIndex(x => x.name.Equals(questName)) + 1].isActive = true;

        lastCompletedQuestName = questName;

        if (currentQuest.OnQuestCompleted != null) currentQuest.OnQuestCompleted.Raise();
        questCompleted.Raise();

        homeRestCooldown++;
        otherRestCooldown++;
    }

    public bool IsQuestActive(string questName)
    {
        var quest = quests.Find(x => x.name.Equals(questName));
        return quest.isActive;
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

    public void AddRestCharges(int value)
    {
        restCharges += value;
    }
    
    public void SaveQuests()
    {
        ES3.Save("Quests", this);
    }

    public void Rest(bool isHome)
    {
        if (isHome)
        {
            restCharges += 3;
            homeRestCooldown = 0;
        }
        else
        {
            restCharges += 2;
            otherRestCooldown = 0;
        }
    }
    
}

[System.Serializable]
public struct RestCooldowns
{
    public int cofix;
    public int kfc;
    public int home;
}
