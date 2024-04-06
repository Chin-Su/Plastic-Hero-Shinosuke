using UnityEngine;

public class DoorKey : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (GameManager.Instance.GetCard() > 0))
        {
            animator.SetTrigger("isOpened");
        }
    }
}