using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pelmeni : MonoBehaviour
{
    public float time;
    private float currentTime = 0f;

    public Slider timerSlider;
    public TMP_Text secondsRemainText;
    public DialogueTrigger startDialogue;
    public ParticleSystem pelmeniParticles;
    public Transform playerPosition;
    public GameObject loseText;
    public SceneLoader sceneLoader;

    [Header("Quests")] 
    
    public Quests quests;
    public string questName;
    
    private InputMaster controls;
    private IEnumerator timeCoroutine;
    
    public void Start()
    {
        controls = new InputMaster();
        startDialogue.TriggerDialogue();
        timeCoroutine = Timer();
    }

    private void Update()
    {
        timerSlider.value = CalculateTime();
        secondsRemainText.text = $"Осталось продержаться: {time - currentTime:f2} сек.";

        if (currentTime.Equals(time))
            StartCoroutine(WinCoroutine());
    }

    private IEnumerator WinCoroutine()
    {
        quests.QuestCompleted(questName);
        var col = pelmeniParticles.collision;
        col.enabled = false;
        pelmeniParticles.Stop();
        yield return new WaitForSeconds(2f);
        sceneLoader.LoadScene(ES3.Load<string>("CurrentLevelName"));
    }

    public void Lose()
    {
        StartCoroutine(LoseCoroutine());
    }

    private IEnumerator LoseCoroutine()
    {
        loseText.SetActive(true);
        controls.Disable();
        StopCoroutine(timeCoroutine);
        
        yield return new WaitForSeconds(3f);

        currentTime = 0;
        controls.Enable();
        loseText.SetActive(false);
        StartGame();
    }
    
    private void StartGame()
    {
        var col = pelmeniParticles.collision;
        col.enabled = true;

        StartCoroutine(timeCoroutine);
    }
    
    private IEnumerator Timer()
    {
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return WinCoroutine();
    }

    private float CalculateTime()
    {
        return currentTime / time;
    }
}
