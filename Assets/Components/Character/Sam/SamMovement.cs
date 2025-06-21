using UnityEngine;
using UnityEngine.InputSystem;

public class SamMovement : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody2D rb;
    private Animator animator;

    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private float horizontalMovement;
    private bool isGrounded;
    private bool jumpPressed;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.3f;

    public LayerMask pushableLayer;
    public float pushCheckDistance = 0.3f;

    void Awake()
    {
        controls = new PlayerControls();

        // New simpler input bindings
        controls.SamControls.Move.performed += OnMove;
        controls.SamControls.Move.canceled += OnMove;
        controls.SamControls.Jump.performed += OnJump;
    }

    void OnEnable() => controls.SamControls.Enable();
    void OnDisable() => controls.SamControls.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Horizontal movement
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);

        // Jumping
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false; // prevent double jump
        }

        // Flip
        if (horizontalMovement > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalMovement < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsJumping", !isGrounded);

        // Push logic
        bool isPushing = false;

        if (horizontalMovement != 0 && isGrounded)
        {
            Vector2 direction = new Vector2(transform.localScale.x, 0);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, pushCheckDistance, pushableLayer);

            if (hit.collider != null)
                isPushing = true;
        }

        animator.SetBool("IsPushing", isPushing);
    }

    // Called when movement input is performed or cancelled
    public void OnMove(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    // Called once when jump is pressed
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}