using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rigidBodyVar;

    // Reference to the joystick
    public Joystick joystick;

    private Animator animator;

    public float attackRange = 1.2f; // Set the attack range
    public float attackDuration = 0.5f; // Duration of the attack
    private BoxCollider2D attackCollider; // Reference to the attack collider

    private void Start()
    {
        // Get the animator component attached to Player
        animator = GetComponent<Animator>();
        rigidBodyVar = GetComponent<Rigidbody2D>();

        // Reference the attack collider from the child GameObject
        attackCollider = transform.Find("AttackCollider").GetComponent<BoxCollider2D>();
        attackCollider.size = new Vector2(attackRange, attackRange); // Set the attack collider size
        attackCollider.enabled = false; // Disable initially
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
        if (collision.CompareTag("Enemy")) // Make sure enemies are tagged as "Enemy"
        {
            Destroy(collision.gameObject); // Destroy the enemy
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
