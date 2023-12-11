using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isHorizontal;
    private bool isDoubleJump;

    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isHorizontal = true;
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
        }

        // When player jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (OnGround())
                Jump();
        }

        Debug.Log(OnGround());
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

    private void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        animator.SetTrigger("isJumping");
    }

    private bool OnGround()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector2 size = new Vector2(collider.bounds.size.x - 0.02f, collider.bounds.size.y - 0.1f);
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, size, 0, Vector2.down, 0.06f, groundLayer);

        if (hit)
            return true;
        return false;
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