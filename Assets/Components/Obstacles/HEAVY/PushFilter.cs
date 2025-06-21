using UnityEngine;

public class PushFilter : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cat"))
        {
            // Cancel any velocity that Cat might cause
            rb.linearVelocity = Vector2.zero;

            // Optional: freeze X motion completely while Cat touches
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            // Unfreeze so Sam can push it
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
