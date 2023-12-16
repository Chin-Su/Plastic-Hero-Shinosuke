using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isHorizontal;
    private bool isOnWall;
    private bool isDoubleJump;

    [Header("Parameter")]
    [SerializeField] private float runSpeed;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float moveOnWallSpeed;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isHorizontal = true;
        isOnWall = false;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // When player move on the ground
        if (isHorizontal)
        {
            // Set speed and animation walking for player
            rigidbody.velocity = new Vector2(horizontalInput * runSpeed, rigidbody.velocity.y);
            animator.SetFloat("isWalking", Mathf.Abs(horizontalInput));

            SetDirection(horizontalInput);
        }
        else
        {
            rigidbody.gravityScale = 0;
            rigidbody.velocity = new Vector2(0, verticalInput * moveOnWallSpeed);
        }

        // When player jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isDoubleJump)
                Jump();
        }

        UpdateState();
    }

    /// <summary>
    /// Set direction for player
    /// </summary>
    /// <param name="value">Is value when player move to left or right</param>
    private void SetDirection(float value)
    {
        if (value < 0)
            spriteRenderer.flipX = false;
        if (value > 0)
            spriteRenderer.flipX = true;
    }

    /// <summary>
    /// Handle action jump of player
    /// </summary>
    private void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        if (OnGround())
            animator.SetTrigger("isJumping");
        else
        {
            animator.SetBool("isDoubleJumping", true);
            isDoubleJump = false;
        }
    }

    /// <summary>
    /// Check player is on ground or not
    /// </summary>
    /// <returns>true if player is on ground, false if player is not in ground</returns>
    private bool OnGround()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector2 size = new Vector2(collider.bounds.size.x - 0.02f, collider.bounds.size.y - 0.1f);
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, size, 0, Vector2.down, 0.06f, groundLayer);

        if (hit)
            return true;

        return false;
    }

    /// <summary>
    /// Update state when player moving or idle
    /// </summary>
    private void UpdateState()
    {
        // When player in ground, reset all state to origin
        if (OnGround())
        {
            isDoubleJump = true;
            animator.SetBool("isDoubleJumping", false);
            animator.SetBool("isCrashing", false);
        }

        // Handle state when player crashing
        if (rigidbody.velocity.y < -10)
            animator.SetBool("isCrashing", true);
    }

    /**/

    private void OnDrawGizmosSelected()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector2 size = new Vector2(collider.bounds.size.x - 0.05f, collider.bounds.size.y - 0.1f);
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, size, 0, Vector2.down, 0.06f, groundLayer);

        Gizmos.color = Color.red;
        if (hit.collider != null)
        {
            Gizmos.DrawWireCube(hit.point, size);
        }
        else
        {
            Gizmos.DrawWireCube(collider.bounds.center + Vector3.down * 0.06f, size);
        }
    }
}