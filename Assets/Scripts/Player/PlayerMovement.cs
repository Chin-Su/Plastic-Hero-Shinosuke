using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isHorizontal;

    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;

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
        if(isHorizontal)
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
            Jump();
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
}