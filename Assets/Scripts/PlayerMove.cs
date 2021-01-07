using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private float verticalMovement;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        MovePlayer(horizontalMovement, verticalMovement);

        Flip(rb.velocity.x);

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isClimbing", isClimbing);
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        MovePlayer(horizontalMovement, verticalMovement);
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.deltaTime;
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }

        }
        else
        {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
