using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private float damage;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.contacts[0].normal.y == 1)
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

            animator.SetTrigger("isHitting");
        }
    }

    public void UnEnable()
    {
        gameObject.SetActive(false);
    }
}