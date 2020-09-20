using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Heal,
    DealDamage,
    SkipTurn
}

[System.Serializable]
public class Skill
{
    public SkillType skillType;
    public string name;
    public string description;
    public int amount;
    public int manaCost;
    public bool isActive = true;
}
