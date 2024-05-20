using UnityEngine;

public class HeartItem : MonoBehaviour
{
    [SerializeField] private float health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.PostEvent(EventId.EnemyMakeDamage, health);
            gameObject.SetActive(false);
        }
    }
}