using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private GameObject notificationPrefab = null;
    [SerializeField] private GameObject useButton = null;

    [SerializeField] private TMP_Text restChargesText = null;

    public AudioSystem audioSystem;
    public Quests quests;
    public Position playerPosition;
    public bool isPaused;

    private bool isQuestMenuOpened = false;

    private void Update()
    {
        useButton.SetActive(quests.isAbleToLoad);
        restChargesText.text = $"Заряды отдыха: {quests.restCharges}";
    }

    
    public void Pause()
    {
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        isPaused = !isPaused;
        if (isPaused) Player.Instance.controls.Disable();
        else Player.Instance.controls.Enable();
    }

    public void OpenQuestMenu()
    {
        if (isQuestMenuOpened)
        {
            animator.SetTrigger("CloseQuestMenu");
            isQuestMenuOpened = false;
        }
        else
        {
            animator.SetTrigger("OpenQuestMenu");
            isQuestMenuOpened = true;
        }
    }

    public void SpawnQuestNotification()
    {
        var go = Instantiate(notificationPrefab, gameObject.transform);
        go.GetComponentInChildren<TMP_Text>().text = $"Задание \"{quests.lastCompletedQuestName} \" выполнено!";
        audioSystem.Play("QuestCompleted");
    }
}
