using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    [Serializable]
    public class DialogueEvent : UnityEvent {}

    [SerializeField]
    private DialogueEvent onDialogueTriggered = new DialogueEvent();
}
