using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Transform questWindowContent;
    public GameObject questTemplatePrefab;
    public Quests questsRef;
    
    private List<Quest> quests;
    private List<Quest> allQuests;
    private List<Quest> tutorialQuests;


    private void Start()
    {
        quests = questsRef.quests;
        UpdateQuestMenu();
    }

    public void UpdateQuestMenu()
    {
        for (int i = 0; i < questWindowContent.childCount; i++)
        {
            Destroy(questWindowContent.GetChild(i).gameObject);
        }
        
        foreach (var quest in quests)
        {
            DisplayQuest(quest);
        }
    }
    
    public void DisplayQuest(Quest quest)
    {
        var questObject = Instantiate(questTemplatePrefab, questWindowContent);
        questObject.transform.Find("QuestName").GetComponent<TMP_Text>().text = quest.name;
        questObject.transform.Find("Location").GetComponent<TMP_Text>().text = quest.location;
        questObject.transform.name = quest.name;
        
        if (!quest.isActive)
        {
            questObject.SetActive(false);
        }
    }
}
