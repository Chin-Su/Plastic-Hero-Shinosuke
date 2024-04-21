using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public string collisionTag;
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(collisionTag) && !string.IsNullOrEmpty(collisionTag))
            onTriggerEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(collisionTag) && !string.IsNullOrEmpty(collisionTag))
            onTriggerExit?.Invoke();
    }
}