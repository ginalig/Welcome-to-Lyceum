using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RandomNPC : MonoBehaviour
{
    [Header("Appearance")]
    public List<Sprite> hairStyles;
    public List<Color> skinColors;
    public SpriteRenderer hair;
    public SpriteRenderer skin;
    public SpriteRenderer eyes;
    public List<SpriteRenderer> parts;

    [Header("TechStuff")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Values")] 
    public float minSpeed;
    public float maxSpeed;
    private float speed;
    [System.NonSerialized] public Transform leftBorder;
    [System.NonSerialized] public Transform rightBorder;

    private Vector3 tempPos;
    
    private void Start()
    {
        hair.sprite = hairStyles[Random.Range(0, hairStyles.Count)];
        skin.color = skinColors[Random.Range(0, skinColors.Count)];
        hair.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        foreach (var part in parts)
        {
            part.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        
        StartCoroutine(Walk());
    }

    private void Update()
    {
        if (rb.velocity.x > 0) transform.right = Vector3.right;
        else if (rb.velocity.x < 0) transform.right = Vector3.left;

        rb.velocity = new Vector2(speed, 0);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        
        tempPos = transform.position;
        tempPos.x = Mathf.Clamp(tempPos.x, leftBorder.position.x, rightBorder.position.x);
        transform.position = tempPos;
    }

    private IEnumerator Walk()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f) - 1f);
            speed = Random.Range(minSpeed, maxSpeed) * (Random.Range(0, 2) == 0 ? -1 : 1);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            speed = 0;
        }
    }

    public void SetLayer(int number)
    {
        hair.sortingOrder = number + 1;
        eyes.sortingOrder = number - 1;
        skin.sortingOrder = number;
        foreach (var part in parts)
        {
            part.sortingOrder = number;
        }
    }
}
