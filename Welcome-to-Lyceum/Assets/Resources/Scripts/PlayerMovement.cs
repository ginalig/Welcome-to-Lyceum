using System;
using UnityEngine;

namespace Resources.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        
        public Animator animator;
        public Rigidbody2D rb;
        public SpriteRenderer sr;

        public float speed;
        private bool isOnPoster;

        private AudioManager audioManager;
        private GameManager gameManager;
        private Vector3 mousePosition;
        private Vector2 direction;
        private Poster poster;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void FixedUpdate()
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            if (Input.GetKey(KeyCode.D)) direction = Vector2.right;
            if (Input.GetKey(KeyCode.A)) direction = Vector2.left;

            if (Input.GetKey(KeyCode.W)) direction += Vector2.up * (speed * 0.2f);
            if (Input.GetKey(KeyCode.S)) direction += Vector2.down * (speed * 0.2f);
            
            if (Input.GetKeyDown(KeyCode.Escape)) gameManager.Pause();
            
            if (Input.GetKeyDown(KeyCode.E) && isOnPoster)
            {
                audioManager.Play("Interaction");
                Debug.Log(poster.posterText);
            }
            
            animator.SetFloat(Animator.StringToHash("Speed"), rb.velocity.magnitude);

            rb.velocity = direction * speed;

            // if (rb.velocity.x > 0.01f)
            // {
            //     transform.right = Vector3.right;
            // }
            // else if (rb.velocity.x < -0.01f)
            // {
            //     transform.right = Vector3.left;
            // }
            if (mousePosition.x > gameObject.transform.position.x)
                transform.right = Vector3.right;
            else if (mousePosition.x < gameObject.transform.position.x)
                transform.right = Vector3.left;
            
            direction *= 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Poster"))
            {
                isOnPoster = true;
                poster = other.gameObject.GetComponent<Poster>();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Poster"))
            {
                isOnPoster = false;
            }
        }

        private void WalkingSound()
        {
            if (audioManager != null)
                audioManager.Play("Walking");
        }
    }
}
