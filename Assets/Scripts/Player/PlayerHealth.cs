using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startHealth;

    [Header("IFrames")]
    [SerializeField] private int numberOfFlashes;

    [SerializeField] private float iFramesDuration;

    [Header("Layer")]
    [SerializeField] private int layerPlayer;

    [SerializeField] private int layerEnemy;

    [Header("UI")]
    [SerializeField] private Slider healthBar;

    private float currentHealth;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = startHealth;
    }

    private void Start()
    {
        this.RegisterListener(EventId.EnemyMakeDamage, (param) => TakeDamage((float)param));
    }

    /// <summary>
    /// Used to handle logic when player attacked
    /// </summary>
    /// <param name="damage">Damage of enemmy make</param>
    private void TakeDamage(float damage)
    {
        currentHealth += damage;
        Debug.LogError(currentHealth);
        if (currentHealth > 0)
        {
            this.PostEvent(EventId.Attacked);
            StartCoroutine(Invunerability());
        }
        else
            this.PostEvent(EventId.Die);
    }

    /// <summary>
    /// This function used to set time and effect invunerability for player
    /// </summary>
    /// <returns></returns>
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(layerPlayer, layerEnemy, true);
        for (int i = 0; i < numberOfFlashes; ++i)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.85f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(layerPlayer, layerEnemy, false);
    }
}