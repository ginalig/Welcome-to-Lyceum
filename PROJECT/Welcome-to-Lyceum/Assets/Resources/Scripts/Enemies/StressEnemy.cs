using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cinemachine;
using Resources.Scripts;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class StressEnemy : MonoBehaviour
{
    private Player player;

    [SerializeField] private Rigidbody2D rigidBody = null;
    [SerializeField] private Velocity playerRbAsset = null;
    [SerializeField] private Position playerPositionAsset = null;
    private Rigidbody2D playerRigidBody = null;

    [SerializeField] private float dangerDistance = 0f;
    [SerializeField] private float safeDistance = 0f;
    
    private bool isAbleToMove = true;
    
    private static readonly int Shoot1 = Animator.StringToHash("Shoot");

    void Start()
    {
        player = Player.Instance;
        playerRigidBody = playerRbAsset.rb;
    }

    void FixedUpdate()
    {
        if (math.abs(playerPositionAsset.position.x - transform.position.x) <= safeDistance) // Если стресс слишком близко, ничего не делать
            return;
        
        //playerPosition.position = playerPositionAsset.position;
        
        LookAtPlayer();
        
        if (isAbleToMove) Move();
        
    }

    private void LookAtPlayer()
    {
        if (playerPositionAsset.position.x < transform.position.x)
            transform.right = Vector3.left;
        else if (playerPositionAsset.position.x > transform.position.x)
            transform.right = Vector3.right;
    }

    private void Move()
    {
        var playerVelocity = playerRigidBody.velocity;

        if ((playerVelocity.x > 0 && playerPositionAsset.position.x < transform.position.x ||
             playerVelocity.x < 0 && playerPositionAsset.position.x > transform.position.x) &&
            math.abs(playerPositionAsset.position.x - transform.position.x) <= dangerDistance) 
        {
            rigidBody.velocity = (new Vector3(playerPositionAsset.position.x, playerPositionAsset.position.y, 0) - transform.position).normalized * -2;
        }
        else
        {
            rigidBody.velocity = (new Vector3(playerPositionAsset.position.x, playerPositionAsset.position.y, 0) - transform.position).normalized * 4;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, safeDistance);
    }
}
