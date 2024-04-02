using UnityEngine;

public class AngryPig : EnemyMakeDamage
{
    [SerializeField] private bool boss;

    private Animator animator;
    private new Rigidbody2D rigidbody;
    private bool attacking;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!boss)
        {
            if (!attacking)
                animator.SetBool("isWalking", true);
            else
            {
                animator.SetBool("isRunning", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            this.PostEvent(EventId.EnemyMakeDamage, -damage);
    }
}