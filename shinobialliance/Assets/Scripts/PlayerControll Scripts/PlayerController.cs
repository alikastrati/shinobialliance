using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this for using the Image component

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rigidBodyVar;
    public int health = 5; // Start with full health (5)

    // Reference to the joystick
    public Joystick joystick;

    private Animator animator;

    public float attackRange = 1.2f; // Set the attack range
    public float attackDuration = 0.5f; // Duration of the attack
    private BoxCollider2D attackCollider; // Reference to the attack collider

    // Reference to the health bar UI
    public Image healthBarImage; // Drag your HealthBar Image here in the Inspector
    public Sprite[] healthBarSprites; // Array for your health bar sprites

    public GameObject coinPrefab; // Assign the coin prefab in the Inspector


    private void Start()
    {
        // Get the animator component attached to Player
        animator = GetComponent<Animator>();
        rigidBodyVar = GetComponent<Rigidbody2D>();

        // Reference the attack collider from the child GameObject
        attackCollider = transform.Find("AttackCollider").GetComponent<BoxCollider2D>();
        attackCollider.size = new Vector2(attackRange, attackRange); // Set the attack collider size
        attackCollider.enabled = false; // Disable initially
        attackCollider.isTrigger = true; // Ensure this collider is a trigger
    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        // Enable the attack collider
        attackCollider.enabled = true;

        // Play attack animation
        animator.SetTrigger("Attack"); // Ensure this matches the trigger name in the Animator

        // Wait for the duration of the attack
        yield return new WaitForSeconds(attackDuration);

        // Disable the attack collider
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Spawn a coin at the enemy's position
            Instantiate(coinPrefab, collision.transform.position, Quaternion.identity);

            // Destroy the enemy
            Destroy(collision.gameObject);

            // Update score
            ScoreManager.instance.AddScore(1); // Add 1 point for each kill
            Debug.Log("Enemy destroyed! Current Score: " + ScoreManager.instance.score);
        }
        //else if (collision.CompareTag("Coin")) // New condition to handle coin collection
        //{
        //    ScoreManager.instance.AddCoin(); // Increase the coin count
        //    Destroy(collision.gameObject); // Remove the coin
        //    Debug.Log("Coin collected! Total coins: " + ScoreManager.instance.coinsCollected);
        //}
    }





    public void UpdateHealthBar()
    {
        // Update the health bar sprite based on the current health
        if (health >= 0 && health < healthBarSprites.Length)
        {
            healthBarImage.sprite = healthBarSprites[health]; // Set the sprite according to the current health
            Debug.Log($"Health updated to {health}. Sprite set to {healthBarSprites[health].name}.");
        }
        else if (health < 0)
        {
            healthBarImage.sprite = healthBarSprites[0]; // Set to empty health sprite if health is below 0
            Debug.Log("Health below 0. Setting to empty health sprite.");
        }
    }

    private void Update()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        Vector2 move = moveInput * moveSpeed * Time.fixedDeltaTime;
        rigidBodyVar.MovePosition(rigidBodyVar.position + move);
    }

    private void HandleAnimation()
    {
        // Check if the player is moving
        bool isMoving = moveInput.magnitude > 0;

        // Set the IsIdle parameter
        animator.SetBool("IsIdle", !isMoving);

        // Reset all movement parameters
        animator.SetBool("IsMovingUp", false);
        animator.SetBool("IsMovingDown", false);
        animator.SetBool("IsMovingLeft", false);
        animator.SetBool("IsMovingRight", false);

        if (isMoving)
        {
            // Determine the active movement direction
            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
            {
                // Horizontal movement
                animator.SetBool("IsMovingRight", moveInput.x > 0);
                animator.SetBool("IsMovingLeft", moveInput.x < 0);
            }
            else
            {
                // Vertical movement
                animator.SetBool("IsMovingUp", moveInput.y > 0);
                animator.SetBool("IsMovingDown", moveInput.y < 0);
            }
        }
    }
}
