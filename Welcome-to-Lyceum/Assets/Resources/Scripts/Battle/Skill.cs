using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SkillType
{
    Heal,
    DealDamage,
    SkipTurn,
    SelfDamage
}

[System.Serializable]
public class Skill
{
    public SkillType skillType;
    public string name;
    [TextArea(0, 3)]
    public string description;
    public int amount;
    public int manaCost;
    public int cooldown;
    public int currentCooldown;
    public bool isActive = true;
    public bool isRandomized = true;
    public UnityEvent OnUsedEvent;

    public void CheckCooldown()
    {
        if (currentCooldown >= cooldown)
            isActive = true;
        else
            isActive = false;
    }

    public void OnUsed()
    {
        isActive = false;
        currentCooldown = -1;
        OnUsedEvent.Invoke();
    }
}
