using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public bool isTutorial = false;
    

    public Transform questWindowContent;
    public GameObject questTemplatePrefab;
    public GameObject questAccomplishmentNotification;
    public TMP_Text questNotificationText;

    public List<Quest> quests;
    public List<Quest> allQuests;
    public List<Quest> tutorialQuests;
    
    public static QuestManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (!isTutorial) // Если не тутор, то загрузить квесты
        {
            quests = ES3.Load("Quests", allQuests);
        }
        else
        {
            quests = tutorialQuests;
        }
        UpdateQuestMenu(quests);
    }

    public void UpdateQuestMenu(List<Quest> quests)
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

    public void QuestCompleted(string questName)
    {
        var currentQuest = quests.Find(x => x.name.Equals(questName));
        currentQuest.isActive = false;
        questAccomplishmentNotification.SetActive(true);
        questNotificationText.text = $"Задание {questName} выполнено!";
        currentQuest.OnQuestCompleted.Invoke();
        int nextQuestIndex = quests.FindIndex(x => x.name.Equals(questName)) + 1;
        if (nextQuestIndex < quests.Count)
            quests[quests.FindIndex(x => x.name.Equals(questName)) + 1].isActive = true;
        UpdateQuestMenu(quests);
        
        AudioManager.instance.Play("QuestCompleted");
    }
}
