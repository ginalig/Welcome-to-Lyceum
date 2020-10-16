using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

namespace Resources.Scripts
{
    public class Player : MonoBehaviour
    {
        private Vector3 mousePosition = Vector3.zero;

        public Animator animator = null;

        private AudioManager audioManager = null;
        private Vector2 direction = Vector2.zero;
        private GameManager gameManager = null;

        public Rigidbody2D rb;
        public GroundDetection groundDetection;
        public PlayerUI playerUI;
        public InputMaster controls;
        public Transform attackPoint;
        public LayerMask enemyLayers;
        public Slider stressBar;
        public GameObject stressedEffectImage;
        
        public float jumpForce;
        public float speed;
        public float attackRange;
        public float stressLevel;
        public int attackDamage;
        private bool isAbleToAttack = true;
        private int isConfused = -1;
        public Position playerPosition;
        public Position playerStartPosition;

        public bool isTutroial = false;
        
        private bool isInDamageArea = false;

        public static Player Instance;

        private void Awake()
        {
            Instance = this;
            controls = new InputMaster();
            controls.Player.Attack.performed += context => StartCoroutine(Attack());
            controls.Player.Jump.performed += _ => Jump();
            controls.Player.Pause.performed += _ => playerUI.Pause();
            controls.Player.QuestMenuOpen.performed += _ => playerUI.OpenQuestMenu();
            controls.Player.QuestMenuClose.performed += _ => playerUI.OpenQuestMenu();
        }

        private void Start()
        {
            gameManager = GameManager.instance;
            audioManager = AudioManager.instance;

            var startPosition = ES3.Load("PlayerPosition", new Vector3(0, -0.5f));
            transform.SetPositionAndRotation(startPosition, quaternion.identity);
            transform.position = playerStartPosition.position;
        }

        private void Update()
        {
            if (Time.timeScale < 1) return; // если пауза, ничего не делать

            mousePosition = controls.Player.Look.ReadValue<Vector2>();
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            direction = controls.Player.Move.ReadValue<Vector2>() * (isConfused * -1);

            animator.SetFloat(Animator.StringToHash("Speed"), rb.velocity.x * rb.velocity.x);

            direction *= speed;
            direction.y = rb.velocity.y;
            rb.velocity = direction; 

            if (mousePosition.x > gameObject.transform.position.x)
                transform.right = Vector3.right;
            else if (mousePosition.x < gameObject.transform.position.x)
                transform.right = Vector3.left;

            animator.SetBool("Jump", !groundDetection.IsGrounded);
            
            stressBar.value = stressLevel;

            playerPosition.position = transform.position;
        }

        private void PlaySound(string soundName)
        {
            if (audioManager != null)
                audioManager.Play(soundName);
        }

        public void Jump()
        {
            if (!groundDetection.IsGrounded) return;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioManager.Play("Jump");
        }


        public IEnumerator Attack()
        {
            if (EventSystem.current.IsPointerOverGameObject() || !isAbleToAttack) yield break;
            animator.SetTrigger("Attack");
           
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (var enemy in hitEnemies)
            {
                var temp = enemy.gameObject.GetComponent<Enemy>();
                temp.TakeDamage(attackDamage);
            }
            
            if (Random.value >= 0.5) audioManager.Play("Attack1");
            else audioManager.Play("Attack2");
            
             
            isAbleToAttack = false;
            yield return new WaitForSeconds(0.5f); // Cooldown
            isAbleToAttack = true;

        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("DamageArea"))
            {
                isInDamageArea = true;
                if (stressLevel >= 100) StartCoroutine(GetConfused());
                GetStressed();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("DamageArea"))
            {
                isInDamageArea = false;
                StartCoroutine(StartDecreasingStress());
            }
        }

        private void GetStressed()
        {
            stressLevel += Time.deltaTime * 30;
        }

        private IEnumerator StartDecreasingStress()
        {
            yield return new WaitForSeconds(3f);
            
            while (stressLevel > 0 && !isInDamageArea)
            {
                yield return null;
                stressLevel -= Time.deltaTime * 5;
            }
        }

        private IEnumerator GetConfused()
        {
            isConfused = 1;
            var prevSpeed = speed;
            StartCoroutine(GetConfusedSpeed());
            stressedEffectImage.SetActive(true);
            yield return new WaitForSeconds(10);
            StopCoroutine(GetConfusedSpeed());
            stressedEffectImage.SetActive(false);
            speed = 5;
            isConfused = -1;
        }

        private IEnumerator GetConfusedSpeed()
        {
            while (isConfused == 1)
            {
                speed = Random.Range(1, 4);
                yield return new WaitForSeconds(0.5f);
                speed = Random.Range(6, 10);
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void OnEnable()
        {
            SetControlsActive(!isTutroial);
        }

        private void OnDisable()
        {
            SetControlsActive(false);
        }

        public void SetControlsActive(bool value)
        {
            if (value) controls.Enable();
            else controls.Disable();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        
    }
}