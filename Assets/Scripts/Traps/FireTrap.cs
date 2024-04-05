using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header("Firetrap Timers")]
    // Time to active object can damage for player
    [SerializeField] private float activationDelay;

    // Time to know this object will be active for long time?
    [SerializeField] private float activationTime;

    [SerializeField] private float damage;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Variable to check player was trigged this object
    private bool triggered;

    private bool active;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Check if not active, set active equal true
            if (!triggered)
                StartCoroutine(ActiveFiretrap());
            if (active)
                this.PostEvent(EventId.EnemyMakeDamage, -damage);
        }
    }

    /// <summary>
    /// Handle when active this object and set animation for it
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActiveFiretrap()
    {
        triggered = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(activationDelay);
        active = true;
        spriteRenderer.color = Color.white;
        animator.SetBool("isActived", active);
        yield return new WaitForSeconds(activationTime);
        active = false;
        triggered = false;
        animator.SetBool("isActived", active);
    }
}