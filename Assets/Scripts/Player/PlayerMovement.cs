using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
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

    [Header("Sound Effect")]
    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private AudioClip doubleJumpSound;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        isHorizontal = true;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // When player moving on the ground
        if (isHorizontal)
        {
            // Set speed and animation walking for player
            rigidbody.velocity = new Vector2(horizontalInput * runSpeed, rigidbody.velocity.y);
            // Post event when player move
            this.PostEvent(EventId.Walking, Mathf.Abs(horizontalInput));

            SetDirection(horizontalInput);
        }
        // When player moving on the wall
        else
        {
            rigidbody.velocity = new Vector2(0, horizontalInput * moveOnWallSpeed);
            // Post event when player climbing
            this.PostEvent(EventId.ClimbingUp, horizontalInput);
            this.PostEvent(EventId.ClimbingDown, horizontalInput);
        }

        // When player jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isDoubleJump && isHorizontal)
                Jump();
        }

        // When player change mode from move on ground to move on wall
        if (isHorizontal && OnWall() && Input.GetMouseButtonDown(1))
        {
            rigidbody.DOMoveY(transform.position.y + 0.25f, 0.1f).OnComplete(() =>
            {
                isHorizontal = false;
                this.PostEvent(EventId.Climbing, true);
                rigidbody.gravityScale = 0;
                rigidbody.velocity = Vector2.zero;
                DOTween.Kill(rigidbody);
            });
        }

        UpdateState();
    }

    /// <summary>
    /// Set direction for player
    /// </summary>
    /// <param name="value">Is value when player move to left or right</param>
    private void SetDirection(float value)
    {
        var localScale = transform.localScale;
        if (value < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(localScale.x), localScale.y, localScale.z);
        }
        if (value > 0)
            transform.localScale = new Vector3(Mathf.Abs(localScale.x), localScale.y, localScale.z);
    }

    /// <summary>
    /// Handle action jump of player
    /// </summary>
    private void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        if (OnGround())
        {
            this.PostEvent(EventId.Jumping);
            SoundManager.Instance.Play(jumpSound);
        }
        else
        {
            this.PostEvent(EventId.DoubleJumping, true);
            SoundManager.Instance.Play(doubleJumpSound);
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
            this.PostEvent(EventId.DoubleJumping, false);
            this.PostEvent(EventId.Crashing, false);
            this.PostEvent(EventId.Climbing, false);
        }

        // Handle state when player crashing
        if (rigidbody.velocity.y < -10)
            this.PostEvent(EventId.Crashing, true);

        // Handle some different animation
        if (Input.GetMouseButtonDown(2))
            this.PostEvent(EventId.Joking);
    }

    public bool IsOnWall() => !isHorizontal;

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