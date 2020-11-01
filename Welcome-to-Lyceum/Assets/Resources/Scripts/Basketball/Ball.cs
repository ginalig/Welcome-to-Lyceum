using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BasketballManager manager;
    
    public Rigidbody2D rb;
    public CircleCollider2D col;
    
    public Transform groundPosition;
    public Vector3 ballStartPosition;
    
    public Vector3 pos => transform.position;
    
    void Start()
    {
        DeactivateRb();
    }

    void Update()
    {
        if (transform.position.y < groundPosition.position.y) Reset();
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    
    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DeactivateRb()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }

    private void Reset()
    {
        transform.position = ballStartPosition;
        DeactivateRb();
        manager.isAbleToDrag = true;

        if (!manager.isScored)
        {
            manager.LoseStreak();
        }

        manager.isScored = false;
    }

}
