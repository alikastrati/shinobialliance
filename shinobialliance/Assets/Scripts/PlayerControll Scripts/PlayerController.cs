using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rigidBodyVar;

    // Reference to the joystick
    public Joystick joystick;

    private Animator animator;

    private void Start()
    {
        // Get the animator component attached to Player
        animator = GetComponent<Animator>();
        rigidBodyVar = GetComponent<Rigidbody2D>();
            
    }

    //private void Update()
    //{
    //    // Get input from the joystick
    //    moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);

    //    // Move the player based on joystick input
    //    Vector3 move = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
    //    transform.position += move;

    //    HandleAnimation();
    //}

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
                if (moveInput.x > 0)
                {
                    animator.SetBool("IsMovingRight", true);
                }
                else
                {
                    animator.SetBool("IsMovingRight", false);
                }


                if (moveInput.x < 0)
                {
                    animator.SetBool("IsMovingLeft", true);
                }
                else
                {

                    animator.SetBool("IsMovingLeft", false);

                }
            }
            else
            {
                // Vertical movement
                if (moveInput.y > 0)
                {
                    animator.SetBool("IsMovingUp", true);
                }
                else
                {
                    animator.SetBool("IsMovingDown", true);
                }
            }
        }
    }
}
