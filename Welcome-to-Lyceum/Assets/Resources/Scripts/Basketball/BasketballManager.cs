using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BasketballManager : MonoBehaviour
{
    [Header("HUD")]
    
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    [Header("Objects")] 
    
    public Ball ball;
    public Trajectory trajectory;

    [Header("Values")] 
    
    public float force;
    public int score;
    public int highScore;

    [Header("Events")] 
    
    public UnityEvent onScoredEvent;

    private InputMaster controls;
    private bool isDragging;
    
    [HideInInspector] public bool isAbleToDrag = true;
    [HideInInspector] public bool isScored = true; 

    private Vector2 mStartPos;
    private Vector2 mEndPos;
    private Vector2 mDirection;
    private Vector2 mForce;
    private float mDistance;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Attack.performed += _ => OnDragStart();
        controls.Player.Attack.canceled += _ => OnDragEnd();
    }
    
    void Start()
    {
        highScore = ES3.Load("BasketballHighScore", 0);
        highScoreText.text = $"Рекорд: {highScore}";
    }

    private void Update()
    {
        if (isDragging) OnDrag();

        scoreText.text = $"Счет: {score}";
        highScoreText.text = $"Рекорд: {highScore}";
    }

    public void Score()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
            UpdateHighScore();
        }

        isScored = true;
        
        onScoredEvent.Invoke();
    }

    private void UpdateHighScore()
    {
        ES3.Save("BasketballHighScore", highScore);
    }

    public void LoseStreak()
    {
        score = 0;
    }
    
    private void OnDragStart()
    {
        if (!isAbleToDrag) return;
        trajectory.Show();
        isDragging = true;
        ball.DeactivateRb();
        mStartPos = controls.Player.Look.ReadValue<Vector2>();
    }

    private void OnDragEnd()
    {
        if (!isAbleToDrag) return;
        trajectory.Hide();
        isAbleToDrag = false;
        isDragging = false;
        ball.ActivateRb();
        ball.Push(mForce);
    }

    private void OnDrag()
    {
        mEndPos = controls.Player.Look.ReadValue<Vector2>();
        mDistance = Vector2.Distance(mStartPos, mEndPos);
        mDirection = (mStartPos - mEndPos).normalized;
        mForce = mDirection * (mDistance * force * 0.1f);
        trajectory.UpdateDots(ball.pos, mForce, ball.rb.gravityScale);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
