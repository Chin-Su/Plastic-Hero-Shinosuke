using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;

    [Header("Animation")]
    [SerializeField] private string explore;

    private float timeLife;
    private int direction = 1;
    private new Rigidbody2D rigidbody;
    private Animator animator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        timeLife = 3;
    }

    private void Update()
    {
        rigidbody.velocity = Vector2.right * speed * direction;
        timeLife -= Time.deltaTime;
        if (timeLife < 0)
            gameObject.SetActive(false);
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetDirection(0);
        animator.SetTrigger(explore);
    }
}