using UnityEngine;

public class EnemyMakeDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            this.PostEvent(EventId.EnemyMakeDamage, -damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            this.PostEvent(EventId.EnemyMakeDamage, -damage);
    }

    public float GetDamage()
    {
        return damage;
    }
}