using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startHealth;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private bool notAttacked = false;
    [SerializeField] private AudioClip enemyDieSound;

    private float currentHealth;
    private Animator animator;

    private void Awake()
    {
        currentHealth = startHealth;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        sliderHealth.value = currentHealth / startHealth * 10;
    }

    private void Update()
    {
        if (currentHealth == 0)
        {
            SoundManager.Instance.Play(enemyDieSound);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Bullet") && !notAttacked) || collision.CompareTag("Enemy"))
        {
            TakeDamage(collision.GetComponent<EnemyMakeDamage>().GetDamage());
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startHealth);
        sliderHealth.value = currentHealth / startHealth;
        animator.SetTrigger("isHitting");
    }
}