using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public bool isRepeatable;
    public string id;
    public string name;
    public Sprite NPCIcon;
    
    [TextArea(3, 10)]
    public string[] sentences;

    public UnityEvent OnDialogueFinished;
}
