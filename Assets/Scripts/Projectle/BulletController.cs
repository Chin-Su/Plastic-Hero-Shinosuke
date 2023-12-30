using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;

    [Header("Animation")]
    [SerializeField] private string explore;

    private float timeLife;
    private int direction = 1;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (rigidbody != null)
            rigidbody.velocity = Vector2.right * speed * direction;
        timeLife -= Time.deltaTime;
        if (timeLife < 0)
            gameObject.SetActive(false);
    }

    /// <summary>
    /// Used to set direction move for bullet
    /// </summary>
    /// <param name="direction">Enter int value, 1 to move left-right, -1 to move right-left</param>
    public void SetDirection(int direction)
    {
        timeLife = 3;
        this.direction = direction;
        var localScale = transform.localScale;
        if (direction < 0)
            spriteRenderer.flipX = false;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        direction = 0;
        animator.SetTrigger(explore);
    }
}