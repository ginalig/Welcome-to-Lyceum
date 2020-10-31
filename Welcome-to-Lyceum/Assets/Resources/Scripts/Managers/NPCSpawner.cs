using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCSpawner : MonoBehaviour
{
    public GameObject NPCPrefab;
    public Transform leftBorder;
    public Transform rightBorder;
    public int count;
    private List<GameObject> NPCs;
    private Vector3 tempPos;
    
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var huis = Instantiate(NPCPrefab, new Vector3(Random.Range(leftBorder.position.x, rightBorder.position.x), -0.4f), Quaternion.identity);
            var npc = huis.GetComponent<RandomNPC>();
            npc.SetLayer(i * 3);
            npc.leftBorder = leftBorder;
            npc.rightBorder = rightBorder;
        }
    }
}
