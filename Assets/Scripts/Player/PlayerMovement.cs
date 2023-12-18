using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isHorizontal;
    private bool isDoubleJump;
    private BoxCollider2D boxCollider;

    [Header("Parameter")]
    [SerializeField] private float runSpeed;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float moveOnWallSpeed;
    [SerializeField] private float originGravity;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private LayerMask wallLayer;

    [Header("Animation")]
    [SerializeField] private string walking;

    [SerializeField] private string jumping;
    [SerializeField] private string doubleJumping;
    [SerializeField] private string climbing;
    [SerializeField] private string climbingUp;
    [SerializeField] private string climbingDown;
    [SerializeField] private string crashing;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isHorizontal = true;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // When player move on the ground
        if (isHorizontal)
        {
            // Set speed and animation walking for player
            rigidbody.velocity = new Vector2(horizontalInput * runSpeed, rigidbody.velocity.y);
            animator.SetFloat(walking, Mathf.Abs(horizontalInput));

            SetDirection(horizontalInput);
        }
        else
        {
            rigidbody.velocity = new Vector2(0, horizontalInput * moveOnWallSpeed);
            animator.SetFloat(climbingUp, horizontalInput);
            animator.SetFloat(climbingDown, horizontalInput);
        }

        // When player jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isDoubleJump && isHorizontal)
                Jump();
        }

        // When player change mode from move on ground to move on wall
        if (isHorizontal && OnWall() && Input.GetKeyDown(KeyCode.Mouse2))
        {
            isHorizontal = false;
            rigidbody.gravityScale = 0;
            rigidbody.velocity = new Vector2(0, 0.5f);
            animator.SetBool(climbing, true);
            rigidbody.velocity = Vector2.zero;
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
            animator.SetTrigger(jumping);
        else
        {
            animator.SetBool(doubleJumping, true);
            isDoubleJump = false;
        }
    }

    /// <summary>
    /// Check player is on ground or not
    /// </summary>
    /// <returns>true if player is on ground, false if player is not in ground</returns>
    private bool OnGround()
    {
        Vector2 size = new Vector2(boxCollider.bounds.size.x - 0.02f, boxCollider.bounds.size.y - 0.1f);
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, size, 0, Vector2.down, 0.06f, groundLayer);

        Debug.Log("Player is on ground: " + (hit ? true : false));

        if (hit)
            return true;

        return false;
    }

    private bool OnWall()
    {
        Vector2 size = new Vector2(boxCollider.bounds.size.x - 0.02f, boxCollider.bounds.size.y - 0.1f);
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, size, 0, Vector2.zero, float.PositiveInfinity, wallLayer);

        Debug.Log("Player is on wall: " + (hit ? true : false));

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
            isHorizontal = true;
            rigidbody.gravityScale = originGravity;
            animator.SetBool(doubleJumping, false);
            animator.SetBool(crashing, false);
            animator.SetBool(climbing, false);
        }

        // Handle state when player crashing
        if (rigidbody.velocity.y < -10)
            animator.SetBool(crashing, true);
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