using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;

    public GameObject dialogueMenu;
    public TMP_Text textName;
    public TMP_Text text;
    public Image NPCIcon;

    public AudioSystem audioSystem;

    private Dialogue currentDialogue;
    
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        sentences = new Queue<string>();
        
        //DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0;
        dialogueMenu.SetActive(true);
        
        currentDialogue = dialogue;

        textName.text = dialogue.name;

        NPCIcon.sprite = dialogue.NPCIcon;
        
        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        audioSystem.Play("Typing");
        sentence = sentence.Replace("{PlayerName}", PlayerPrefs.GetString("PlayerName", "Лицеист"));
        
        text.text = "";
        foreach (var letter in sentence)
        {
            text.text += letter;
            yield return null;
        }
    }
    
    public void EndDialogue()
    {
        dialogueMenu.SetActive(false);
        Time.timeScale = 1;
        currentDialogue.OnDialogueFinished.Invoke();
        if (currentDialogue.isRepeatable) return; //Если диалог не одноразовый, то не нужно его отключать 
        PlayerPrefs.SetInt(currentDialogue.id + "IsActive", 0);
    }
}
