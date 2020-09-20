using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Lost,
    Won
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleState battleState = BattleState.Start;

    [Header("Player")]
    [SerializeField] private Skill[] playerSkills = null;
    
    private string playerName;
    [SerializeField] private int playerMaxHealth = 100;
    
    private int playerCurrentHealth = 0;
    [SerializeField] private int PlayerCurrentHealth
    {
        get { return playerCurrentHealth; }
        set { playerCurrentHealth = value >= playerMaxHealth ? playerMaxHealth : value; }
    }

    [Header("Enemy")] 
    [SerializeField] private Skill[] enemySkills = null;
    [SerializeField] private int enemyMaxHealth = 100;
    
    private int enemyCurrentHealth = 0;
    [SerializeField] private int EnemyCurrentHealth
    {
        get { return enemyCurrentHealth; }
        set { enemyCurrentHealth = value >= enemyMaxHealth ? enemyMaxHealth : value; }
    }
    
    [Header("UI")]
    [SerializeField] private TMP_Text playerNameUI = null;

    [SerializeField] private TMP_Text dialogueText = null;
    [SerializeField, TextArea(0, 3)] private string startDialogueText = null;

    [SerializeField] private Slider playerHealthBar = null;
    [SerializeField] private Slider playerManaBar = null;
    
    [SerializeField] private Slider enemyHealthBar = null;

    [SerializeField] private GameObject skillButton = null;

    [SerializeField] private Transform scrollContent = null;


    private void Start()
    {
        
        playerName = PlayerPrefs.GetString("PlayerName", "Лицеист");
        startDialogueText = startDialogueText.Replace("{PlayerName}", playerName);
        
        battleState = BattleState.Start;

        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        playerNameUI.text = playerName;
        dialogueText.text = startDialogueText;
        
        yield return new WaitForSeconds(2f);
        
        battleState = BattleState.PlayerTurn;
        UpdateSkillsList();
        PlayerTurn();
    }

    public void PlayerTurn()
    {
        if (battleState != BattleState.PlayerTurn) return;

        dialogueText.text = "Выбери способность:";
    }

    private void UpdateSkillsList()
    {
        for (int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }

        foreach (var skill in playerSkills)
        {
            if (!skill.isActive || playerManaBar.value < skill.manaCost) continue;
            
            var skillTemplate = Instantiate(skillButton, scrollContent);
            skillTemplate.transform.Find("SkillDescription").GetComponent<TMP_Text>().text = skill.description;
            skillTemplate.transform.Find("ManaCost").GetComponent<TMP_Text>().text = skill.manaCost.ToString();
            skillTemplate.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(Action(skill)));
        }
    }

    private IEnumerator Action(Skill skill)
    {
        if (battleState == BattleState.PlayerTurn)
        {
            dialogueText.text = skill.description;

            if (skill.skillType == SkillType.DealDamage)
            {
                playerManaBar.value -= skill.manaCost;
                
            }
        }

        yield return new WaitForSeconds(3f);
    }

}
