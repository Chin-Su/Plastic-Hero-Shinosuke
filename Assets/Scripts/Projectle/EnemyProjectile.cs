using UnityEngine;

public class EnemyProjectile : EnemyMakeDamage
{
    // Speed of projectile
    [SerializeField] private float speed;

    // Period to fire next bullet
    [SerializeField] private float resetTime;

    // Time life of bullet before it was be un-active
    private float timeLife;

    public void ActivateProjectile()
    {
        // Reset time life
        timeLife = 0;
        // Set active for object
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        timeLife += Time.deltaTime;
        // Check time life and un-active object
        if (timeLife > resetTime)
            gameObject.SetActive(false);
    }

    /// <summary>
    ///  Method to handle if object interactive with other collision
    /// </summary>
    /// <param name="collision"></param>
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        // Call method of parent
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}