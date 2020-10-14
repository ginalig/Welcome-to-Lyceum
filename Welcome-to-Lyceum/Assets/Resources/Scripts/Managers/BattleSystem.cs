using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    [SerializeField, TextArea(0, 3)] private string winDialogueText = null;
    [SerializeField, TextArea(0, 3)] private string loseDialogueText = null;
    
    [SerializeField] private Slider playerHealthBar = null;
    [SerializeField] private Slider playerManaBar = null;
    
    [SerializeField] private Slider enemyHealthBar = null;

    [SerializeField] private GameObject skillButton = null;

    [SerializeField] private Transform scrollContent = null;


    private void Start()
    {
        
        playerName = PlayerPrefs.GetString("PlayerName", "Лицеист");
        startDialogueText = startDialogueText.Replace("{PlayerName}", playerName);
        PlayerCurrentHealth = PlayerPrefs.GetInt("PlayerCurrentHealth", playerMaxHealth);
        
        battleState = BattleState.Start;

        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        playerNameUI.text = playerName;
        dialogueText.text = startDialogueText;
        playerHealthBar.value = PlayerCurrentHealth;

        EnemyCurrentHealth = enemyMaxHealth;
        
        yield return new WaitForSeconds(2f);
        
        battleState = BattleState.PlayerTurn;
        
        yield return PlayerTurn();
    }

    public IEnumerator PlayerTurn()
    {
        if (battleState != BattleState.PlayerTurn) yield break;

        dialogueText.text = "Выбери способность:";
        
        UpdateSkillsList();
        
        yield return new WaitForSeconds(2); 
    }

    public IEnumerator EnemyTurn()
    {
        if (battleState != BattleState.EnemyTurn) yield break;

        dialogueText.text = "Ход противника...";
        
        yield return new WaitForSeconds(1);

        yield return Action(GetRandomSkill(enemySkills));
    }
    

    private IEnumerator Action(Skill skill)
    {
        dialogueText.text = skill.description;

        bool isNextPlayerTurn = false;
        if (battleState == BattleState.PlayerTurn)
        {
            if (skill.skillType == SkillType.DealDamage)
            {
                EnemyTakeDamage(skill.amount);
            }

            if (skill.skillType == SkillType.Heal)
            {
                PlayerTakeHeal(skill.amount);
            }

            if (skill.skillType == SkillType.SkipTurn)
            {
                dialogueText.text += " Противник пропускает ход.";

                isNextPlayerTurn = true;
            }
            else
            {
                isNextPlayerTurn = false;
            }
            
            playerManaBar.value -= skill.manaCost;
        }
        
        else if (battleState == BattleState.EnemyTurn)
        {
            if (skill.skillType == SkillType.DealDamage)
            {
                PlayerTakeDamage(skill.amount);
            }

            if (skill.skillType == SkillType.Heal)
            {
                EnemyTakeHeal(skill.amount);
            }

            if (skill.skillType == SkillType.SkipTurn)
            {
                dialogueText.text += " Ты пропускаешь ход.";

                isNextPlayerTurn = false;
            }
            else
            {
                isNextPlayerTurn = true;
            }
        }
        
        skill.OnUsed();
        
        UpdateSkillCooldowns();
        
        ClearSkillsList();
        
        yield return new WaitForSeconds(3f);

        playerManaBar.value += 5;

        if (isNextPlayerTurn)
        {
            battleState = BattleState.PlayerTurn;
            StartCoroutine(PlayerTurn());
        }
        else
        {
            battleState = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }
    
    private bool PlayerTakeDamage(int amount)
    {
        dialogueText.text += $" Тебе нанесли {amount} урона!";
        
        PlayerCurrentHealth -= amount;
        playerHealthBar.value = PlayerCurrentHealth;
        
        if (PlayerCurrentHealth <= 0)
            return true;
        else
            return false;
    }

    private void PlayerTakeHeal(int amount)
    {
        dialogueText.text += $" Восстановлено {amount} здоровья!";
        
        PlayerCurrentHealth += amount;
        playerHealthBar.value = PlayerCurrentHealth;

    }

    private bool EnemyTakeDamage(int amount)
    {
        dialogueText.text += $" Противнику нанесено {amount} урона!";
        
        EnemyCurrentHealth -= amount;
        enemyHealthBar.value = EnemyCurrentHealth;

        if (EnemyCurrentHealth <= 0)
            return true;
        else
            return false;
    }

    private void EnemyTakeHeal(int amount)
    {
        dialogueText.text += $" Противник восстановил {amount} здоровья!";

        EnemyCurrentHealth += amount;
        enemyHealthBar.value = EnemyCurrentHealth;
        
    }

    private void Win()
    {
        dialogueText.text = "Победа! " + winDialogueText;
    }

    private void Lose()
    {
        dialogueText.text = "Поражение! " + loseDialogueText;
    }

    private Skill GetRandomSkill(Skill[] skills)
    {
        return skills[Random.Range(0, skills.Length - 1)];
    }
    
    private void UpdateSkillsList()
    {
        ClearSkillsList();
        
        foreach (var skill in playerSkills)
        {
            if (!skill.isActive || playerManaBar.value < skill.manaCost) continue;
            
            // Генерация кнопки способности 
            var skillTemplate = Instantiate(skillButton, scrollContent);
            skillTemplate.transform.Find("SkillDescription").GetComponent<TMP_Text>().text = skill.name;
            skillTemplate.transform.Find("ManaCost").GetComponent<TMP_Text>().text = skill.manaCost.ToString();
            skillTemplate.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(Action(skill)));
        }
    }

    private void ClearSkillsList() //Очистка списка способностей
    {
        for (int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
    }

    private void UpdateSkillCooldowns() // Обновление кулдаунов всех способностей, происходит после каждого хода
    {
        foreach (var skill in playerSkills)
        {
            skill.currentCooldown++;
            skill.CheckCooldown();
        }

        foreach (var skill in enemySkills)
        {
            skill.currentCooldown++;
            skill.CheckCooldown();
        }
    }
}
