using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Animator animator;
    [SerializeField] private AudioClip checkPointSound;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            animator.SetTrigger("isChecked");
            SoundManager.Instance.Play(checkPointSound);
        }
    }
}