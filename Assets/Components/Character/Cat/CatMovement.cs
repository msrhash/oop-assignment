using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody2D rb;
    private Animator animator;

    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private float horizontalMovement;
    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.3f;

    void Awake()
    {
        controls = new PlayerControls();

        controls.CatControls.Move.performed += ctx => horizontalMovement = ctx.ReadValue<Vector2>().x;
        controls.CatControls.Move.canceled += ctx => horizontalMovement = 0;

        controls.CatControls.Jump.performed += ctx => Jump();
    }

    void OnEnable() => controls.CatControls.Enable();
    void OnDisable() => controls.CatControls.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsJumping", !isGrounded);

        if (horizontalMovement > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalMovement < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Jump()
    {
        Debug.Log("Cat trying to jump");
        if (isGrounded)
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

