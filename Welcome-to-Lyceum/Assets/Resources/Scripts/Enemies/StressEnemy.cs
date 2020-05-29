using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;

public class StressEnemy : MonoBehaviour
{
    private GameObject player;
    private Transform playerPosition;
    private GameObject spit = null;

    [SerializeField] private Transform spawnSpitPoint = null;
    [SerializeField] private GameObject spitPrefab = null;

    [SerializeField] private Animator animator = null;
    [SerializeField] private float dangerDistance = 0f;
    [SerializeField] private float spitSpeed = 0f;
    
    private static readonly int Shoot1 = Animator.StringToHash("Shoot");

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        playerPosition = player.transform;
        if (playerPosition.position.x < transform.position.x)
            transform.right = Vector3.left;
        else if (playerPosition.position.x > transform.position.x)
            transform.right = Vector3.right;

        if (Mathf.Abs(transform.position.x - playerPosition.position.x) <= dangerDistance)
        {
            animator.SetTrigger(Shoot1);
        }
    }

    private void Shoot()
    {
        spit = Instantiate(spitPrefab, spawnSpitPoint.position, Quaternion.identity);
        var rb = spit.GetComponent<Rigidbody2D>();
        var right = transform.right;
        rb.velocity = right * spitSpeed;
        spit.transform.right = right;
        Destroy(spit, 3f);
    }
    
}
