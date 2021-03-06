﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Resources.Scripts;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{

    [SerializeField] private Shaking shaking = null;
    [SerializeField] private Quests quests = null;

    public BoolAsset isAbleToSpawn;
    public AudioSystem audioSystem;

    public int maxHp;
    private int currentHp;

    void Start()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        if (currentHp <= 0) Die();
    }

    public void TakeDamage(int damage)
    {
        if (quests.restCharges == 0) damage = 0;
        currentHp -= damage;

        // if (gameObject.CompareTag("Stress"))
        // {
        //     var stress = gameObject.GetComponent<StressEnemy>();
        //     StartCoroutine(stress.StopMovingForSeconds(shaking._time));
        // }

        shaking.Begin();
        
        Debug.Log(currentHp);
        
        audioSystem.Play("HitEnemy");
    }

    private void Die()
    {
        foreach (var quest in quests.quests)
        {
            if (quest.isActive && quest.questGoal.goalType == GoalType.Kill)
            {
                quest.questGoal.EnemyKilled();
                if (quest.questGoal.IsReached())
                {
                    quests.QuestCompleted(quest.name);
                }
            }
        }
        
        isAbleToSpawn.SetValue(true);

        quests.restCharges--;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        currentHp = maxHp;
    }
}
