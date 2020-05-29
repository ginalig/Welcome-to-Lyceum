using UnityEngine;

namespace Resources.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private static Vector3 mousePosition;

        public Animator animator;

        private AudioManager audioManager;
        private Vector2 direction;
        private GameManager gameManager;
        public Rigidbody2D rb;

        public float speed;
        public SpriteRenderer sr;

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

            if (Input.GetKey(KeyCode.W)) direction += Vector2.up * 0.5f;
            if (Input.GetKey(KeyCode.S)) direction += Vector2.down * 0.5f;

            animator.SetFloat(Animator.StringToHash("Speed"), rb.velocity.magnitude);

            rb.velocity = direction * speed; // умножение вектора силы персонажа на направление и скорость

            if (mousePosition.x > gameObject.transform.position.x)
                transform.right = Vector3.right;
            else if (mousePosition.x < gameObject.transform.position.x)
                transform.right = Vector3.left;

            direction = Vector2.zero;
        }

        private void PlaySound(string soundName)
        {
            if (audioManager != null)
                audioManager.Play(soundName);
        }
    }
}