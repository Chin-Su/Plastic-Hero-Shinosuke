using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    // Time to re-attack
    [SerializeField] private float attackCooldown;

    // Location to spawn projectile
    [SerializeField] private Transform firePoint;

    // Use pooling to make attack for object
    [SerializeField] private GameObject[] projectiles;

    private float cooldownTimer;

    /// <summary>
    /// Attack function, called when object attack
    /// </summary>
    protected void Attack()
    {
        // Reset cooldown timer
        cooldownTimer = 0;
        // Get bullet and set position for it to fire point
        projectiles[FindProjectile()].transform.position = firePoint.position;
        // Set direction move for bullet
        projectiles[FindProjectile()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    /// <summary>
    /// Function to find index of projectiles in array bullet available
    /// </summary>
    /// <returns>index of bullet is un-active</returns>
    private int FindProjectile()
    {
        for (int i = 0; i < projectiles.Length; ++i)
            if (!projectiles[i].activeInHierarchy)
                return i;
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}