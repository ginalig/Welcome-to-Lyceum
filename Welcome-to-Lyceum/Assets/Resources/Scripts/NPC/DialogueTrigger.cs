using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        if (PlayerPrefs.GetInt(dialogue.id + "IsActive", 1) == 0) return;
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
