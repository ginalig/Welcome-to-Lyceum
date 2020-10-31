using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPelmeni : MonoBehaviour
{
    private InputMaster controls = null;

    public Rigidbody2D rb;
    public Animator animator;
    public float speed;
    public float dashSpeed;
    public float dashTime;
    
    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Dash.performed += _ => StartCoroutine(Dash());
    }

    private void Update()
    {
        if (Time.timeScale < 1) return; // если пауза, ничего не делать

        var mousePosition = controls.Player.Look.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
        var direction = controls.Player.Move.ReadValue<Vector2>();

        animator.SetFloat(Animator.StringToHash("Speed"), rb.velocity.x * rb.velocity.x);

        direction *= speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction; 

        if (mousePosition.x > gameObject.transform.position.x)
            transform.right = Vector3.right;
        else if (mousePosition.x < gameObject.transform.position.x)
            transform.right = Vector3.left;
    }

    private IEnumerator Dash()
    {
        var temp = speed;
        speed *= dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = temp;
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
