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
    private Transform playerPosition;
    private GameObject spit = null;

    [SerializeField] private Transform spawnSpitPoint = null;
    [SerializeField] private GameObject spitPrefab = null;
    [SerializeField] private Rigidbody2D rigidBody = null;
    [SerializeField] private Rigidbody2D playerRigidBody = null;

    [SerializeField] private float dangerDistance = 0f;
    [SerializeField] private float safeDistance = 0f;
    [SerializeField] private float spitSpeed = 0f;

    private bool isAbleToMove = true;
    
    private static readonly int Shoot1 = Animator.StringToHash("Shoot");

    void Start()
    {
        player = Player.Instance;
    }

    void FixedUpdate()
    {
        if (math.abs(player.transform.position.x - transform.position.x) <= safeDistance) // Если стресс слишком близко, ничего не делать
            return; 
        
        playerPosition = player.transform;
        
        LookAtPlayer();
        
        if (isAbleToMove) Move();
        
    }

    public IEnumerator StopMovingForSeconds(float seconds)
    {
        isAbleToMove = false;
        yield return new WaitForSeconds(seconds);
        isAbleToMove = true;
    }

    private void LookAtPlayer()
    {
        if (playerPosition.position.x < transform.position.x)
            transform.right = Vector3.left;
        else if (playerPosition.position.x > transform.position.x)
            transform.right = Vector3.right;
    }

    private void Move()
    {
        var playerVelocity = playerRigidBody.velocity;

        if ((playerVelocity.x > 0 && playerPosition.position.x < transform.position.x ||
             playerVelocity.x < 0 && playerPosition.position.x > transform.position.x) &&
            math.abs(playerPosition.position.x - transform.position.x) <= dangerDistance) 
        {
            rigidBody.velocity = (playerPosition.position - transform.position).normalized * -2;
        }
        else
        {
            rigidBody.velocity = (playerPosition.position - transform.position).normalized * 4;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, safeDistance);
    }
}
